using Company.G05.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.G05.DAL.Data.Configurations;

public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");
        builder.HasOne(E => E.Department)
            .WithMany(D => D.Employees)
            .HasForeignKey(E => E.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);
        // throw new NotImplementedException();
    }
}