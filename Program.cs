
using Day4API.Models;
using Day4API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Day4API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext to the container
            builder.Services.AddDbContext<CompanyContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<EmployeeRepository>();
            //builder.Services.AddScoped<GenericRepository<Employee>>(); // Add Generic Repository to the container for Employee to use it in EmployeeController Constructor
            //builder.Services.AddScoped<GenericRepository<Department>>(); // Add Generic Repository to the container for Department to use it in DepartmentController Constructor
            builder.Services.AddScoped<UnitOfWork>(); // Add UnitOfWork to the container to use it in UnitedController Constructor

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
