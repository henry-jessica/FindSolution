using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductModel;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ProductModel
{
    public class GRNRepository : IGRN<GRN>, IDisposable
   
        {
            ProductDBContext context = new ProductDBContext();

            public GRNRepository(ProductDBContext context)
            {
                this.context = context;
            }

            public void Add(GRN entity)
            {
                context.GRNs.Add(entity);
            }

            public void AddRange(IEnumerable<GRN> entities)
            {
                context.AddRange(entities);
            }

            public IEnumerable<GRN> Find(Expression<Func<GRN, bool>> predicate)
            {
                return context.GRNs.Find(predicate) as IEnumerable<GRN>;
            }

            public GRN Get(int id)
            {
                return context.GRNs.Find(id);
            }

            public IEnumerable<GRN> GetAll()
            {
                return context.GRNs;
            }

            public void Remove(GRN entity)
            {
                context.GRNs.Remove(entity);
            }

            public void RemoveRange(IEnumerable<GRN> entities)
            {
                context.GRNs.RemoveRange(entities);
            }

            public void Dispose()
            {
                context.Dispose();
            }
        }
    }