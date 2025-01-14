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
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
