using Company.BLL.Interfaces;
using Company.DAL.Data;
using Company.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _dbContext;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
        =>   _dbContext.Add(entity); //EF Core 3.1  
         

        public void Delete(T entity)
        =>   _dbContext.Set<T>().Remove(entity);
         

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>)_dbContext.Employees.Include(e => e.Department).AsNoTracking().ToList();
            else
        return _dbContext.Set<T>().AsNoTracking().ToList();
            
        }


        public T GetById(int id)
        =>  _dbContext.Find<T>(id);        
        

        public void Update(T entity)
        => _dbContext.Set<T>().Update(entity);
         

        
    }
}
