using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductModel
{
    public static class DBHelper
    {
        //public static List<T> Get<T>(string resourceName)
        //{
        //    {
        //        using (StreamReader reader = new StreamReader(resourceName, Encoding.UTF8))
        //        {
        //            CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        //            csvReader.Context.RegisterClassMap<Map>();
        //            return csvReader.GetRecords<T>().ToList();
        //        }
        //    }
        //}

        public static List<T> Get<T, S>(string resourceName) where T : class where S : ClassMap<T>
        {
            // Get the current assembly
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {   // create a stream reader
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                    { HasHeaderRecord = true };
                    // create a csv reader dor the stream
                    CsvReader csvReader = new CsvReader(reader, configuration);
                    csvReader.Context.RegisterClassMap<S>();
                    return csvReader.GetRecords<T>().ToList();
                }
            }
        }



    }
}



//List<GRN> gRNS = new List<GRN>();
//modelBuilder.Entity<GRN>().HasData(gRNS);
//List<GRN> csv_gRNS = DBHelper.Get<GRN, GRNMap>("ProductModel.GRN.csv");
//modelBuilder.Entity<GRN>().HasData(csv_gRNS);
//base.OnModelCreating(modelBuilder);
//base.OnModelCreating(modelBuilder);


//modelBuilder.Entity<GRN>().HasData(
//new GRN { GrnID = 3, /* other property values */ },
//new GRN { GrnID = 4, /* other property values */ }
//// other seed entities
//);

//List<GRNLine> grnLines = new List<GRNLine>();
//modelBuilder.Entity<GRNLine>().HasData(grnLines);
//List<GRNLine> cvs_grnLines = DBHelper.Get<GRNLine, GRNLineMap>("ProductModel.GRNLine.csv");
//modelBuilder.Entity<GRNLine>().HasData(cvs_grnLines);
//base.OnModelCreating(modelBuilder);
//base.OnModelCreating(modelBuilder);
