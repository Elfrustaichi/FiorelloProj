using Fiorello.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly FiorelloDbContext _context;

        public Repository(FiorelloDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> exp, params string[] includes)
        {
            var query=_context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query=query.Include(include);
            }

            return query.FirstOrDefault(exp);
        }

        public List<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> exp, params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var item in includes)
                query = query.Include(item);

            return query.Where(exp).ToList();
        }

        public IQueryable<T> GetAllQueryable(System.Linq.Expressions.Expression<Func<T, bool>> exp, params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var item in includes)
                query = query.Include(item);

            return query;
        }

        public bool IsExist(System.Linq.Expressions.Expression<Func<T, bool>> exp, params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var item in includes)
                query = query.Include(item);

            return query.Any(exp);
        }
    }
}
