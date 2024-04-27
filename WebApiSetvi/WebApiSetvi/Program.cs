using Microsoft.Extensions.Configuration;
using WebApiSetvi.Model.Validation;
using WebApiSetvi.Repository;
using WebApiSetvi.Repository.Interfaces;
using WebApiSetvi.Service;
using WebApiSetvi.Service.Interfaces;

namespace WebApiSetvi
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
			{
				//Disable automatic 400 response - custom response is of the type Result
				options.SuppressModelStateInvalidFilter = true;
			});
			string connectionString = builder.Configuration.GetConnectionString("Default");
			builder.Services.AddScoped<ICompanyUserRepository>(provider => new CompanyUserRepository(connectionString));
			//.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<ICompanyUserService, CompanyUserService>();
			builder.Services.AddScoped<ValidationFilterAttribute>();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

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