using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductModel;
using ProductSeeding;
using ProductWepAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProductWepAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddJwtBearer(cfg =>
        {
            
            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration["Tokens:Issuer"],
                ValidateAudience = true,
                ValidAudience = Configuration["Tokens:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
            };
        });

            services.AddDbContext<ProductDBContext>();
            services.AddDbContext<ApplicationDbContext>();

            // As AddDbContext has type scoped we should Add the associated repoitory as scoped
            // Scoped For web applications, a scoped lifetime indicates that services are created once per client request
            // so services.AddTransient<IProduct<Product>, ProductRepository>(); becomes 
            services.AddScoped<IProduct<Product>, ProductRepository>();
            services.AddScoped<IGRN<GRN>,GRNRepository>();

            // This is transient because it is only done once and then forgotten
            services.AddTransient<DbSeeder>();
            services.AddTransient<ApplicationDbSeeder>();
            // This says we are using Controller which are services

            services.AddCors(options =>
            {
                options.AddPolicy(name: LocalAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins("http://localhost:44377", "https://localhost:7026")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; 
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductWepAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat ="JWT",
                    In = ParameterLocation.Header,
                    Description ="JWT Authorization Header using Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    }, new String[] {} 
                }
                });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductWepAPI v1");
                
            }
            );

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();
            app.UseCors(LocalAllowSpecificOrigins);
                        
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        readonly string LocalAllowSpecificOrigins = "_localAllowSpecificOrigins";

    }
}
