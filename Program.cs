
using Day4API.Models;
using Day4API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            builder.Services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CompanyAPI",
                    Version = "v1",
                    Description = "Web API tool that you can use and test",
                    TermsOfService = new Uri("https://github.com/abdullahazmy/WorkingWithAPIs"),
                    Contact = new OpenApiContact
                    {
                        Name = "Abdullah Azmy",
                        Email = "abdullahazmy2@gmail.com",
                    }
                });
                op.EnableAnnotations();
            });

            // Add DbContext to the container
            builder.Services.AddDbContext<CompanyContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<EmployeeRepository>();
            //builder.Services.AddScoped<GenericRepository<Employee>>(); // Add Generic Repository to the container for Employee to use it in EmployeeController Constructor
            //builder.Services.AddScoped<GenericRepository<Department>>(); // Add Generic Repository to the container for Department to use it in DepartmentController Constructor
            builder.Services.AddScoped<UnitOfWork>(); // Add UnitOfWork to the container to use it in UnitedController Constructor

            builder.Services.AddAuthentication(op => op.DefaultAuthenticateScheme = "TokenSchema")
                .AddJwtBearer("TokenSchema", action =>
                {
                    action.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MYSECRETKEYISSANDsoil123@#!@and here am creating my scret key to test it hahahah"))
                    };
                });

            // Allow CORS Policy to allow all origins
            const string myCorsPolicy = "AllowAll";
            builder.Services.AddCors(op =>
            {
                op.AddPolicy(myCorsPolicy, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(myCorsPolicy);
            app.MapControllers();

            app.Run();
        }
    }
}
