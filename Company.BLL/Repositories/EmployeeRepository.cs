using Company.BLL.Interfaces;
using Company.DAL.Data;
using Company.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
       
        public EmployeeRepository(AppDbContext dbContext):base(dbContext)
        {
            
        }
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            var empadd = _dbContext.Employees.Where(a=>a.Adderss.ToLower().Contains(address.ToLower()));
            return empadd;
        }

        public IQueryable<Employee> SearchByName(string name)
        => _dbContext.Employees.Where(E=>E.Name.ToLower().Contains(name));
    }
}
