
using Karadul.Data.Contexts;
using Karadul.Data.Repository;
using Karadul.Data.Repository.AuthRepositories;
using Karadul.Data.Repository.ProductRepositories;
using Karadul.Services.Services.AuthServices;
using Karadul.Services.Services.ProductServices;
using Microsoft.EntityFrameworkCore;

namespace Karadul.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<KaradulDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped  (typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped <IProductRepository, ProductRepository> ();
            builder.Services.AddScoped <IAuthRepository, AuthRepository> ();

            builder.Services.AddScoped <IProductService, ProductService> ();
            builder.Services.AddScoped <IAuthService, AuthService> ();

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
