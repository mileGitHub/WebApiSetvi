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

			builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
			{
				//Disable automatic 400 response - custom response is of the type Result
				options.SuppressModelStateInvalidFilter = true;
			});
			string connectionString = builder.Configuration.GetConnectionString("Default");
			//CompanyUserRepository is used for conection with MS Sql server
			//In case that we want to made connection with different DB provider,
			//we can made another class to implement ICompanyUserRepository (OracleCompanyUserRepository : ICompanyUserRepository)
			//builder.Services.AddScoped<ICompanyUserRepository>(provider => new OracleCompanyUserRepository(connectionStringOracle));
			builder.Services.AddScoped<ICompanyUserRepository>(provider => new CompanyUserRepository(connectionString));
			builder.Services.AddScoped<ICompanyUserService, CompanyUserService>();
			builder.Services.AddScoped<ValidationFilterAttribute>();
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