using Company.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e=>e.Salary).HasColumnType("decimal(18,2)");
            builder.Property(e => e.CreationDate).HasDefaultValueSql("GetDate()");
            builder.Property(E => E.Name)
                .IsRequired(true)
                .HasMaxLength(50); 
        }
    }
}
