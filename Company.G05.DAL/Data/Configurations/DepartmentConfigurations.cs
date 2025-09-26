using Company.G05.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.G05.DAL.Data.Configurations;

public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        // throw new NotImplementedException();
        builder.Property(D => D.Id).UseIdentityColumn(10, 10);
    }
}