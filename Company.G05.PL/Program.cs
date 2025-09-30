using Company.G05.BLL.Interfaces;
using Company.G05.BLL.Repositories;
using Company.G05.DAL.Data.Contexts;
using Company.G05.PL.Mapping;
using Company.G05.PL.Services;
using Microsoft.EntityFrameworkCore;

namespace Company.G05.PL;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        // builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        // builder.Services.AddScoped<DepartmentRepository>();
        // builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        // builder.Services.AddScoped<EmployeeRepository>();
        builder.Services.AddDbContext<CompanyDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        // DI types & life time
        // builder.Services.AddScobed();     // create object life time per request
        // builder.Services.AddTransient();     // create object life time per operation
        // builder.Services.AddSingleton();     // create object life time per application
        
        builder.Services.AddScoped<IScopedService, ScopedService>();
        builder.Services.AddTransient<ITransentService, TransentService>();
        builder.Services.AddSingleton<ISingletonService, SingletonService>();

        builder.Services.AddAutoMapper(M => M.AddProfile( new EmployeeProfile()));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        // app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Employee}/{action=Index}/{id?}");

        app.Run();
    }
}
