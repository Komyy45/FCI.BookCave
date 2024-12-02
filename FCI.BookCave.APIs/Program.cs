using System.Reflection;
using FCI.BookCave.Controllers;
using FCI.BookCave.Persistence;
using FCI.BookCave.Persistence.Identity;

namespace FCI.BookCave.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers().AddApplicationPart(FCI.BookCave.Controllers.AssemblyInformation.Assembly);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddPersistenceServices(builder.Configuration);

			var app = builder.Build();

			#region InitalizeDb
			
			var scope = app.Services.CreateScope();

			var identityDbInitializer = scope.ServiceProvider.GetRequiredService<IdentityDbContextInitializer>();

			try
			{
				await identityDbInitializer.InitializeAsync();
				await identityDbInitializer.SeedAsync();
			}
			catch (Exception ex)
			{
				var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

				logger.LogError(ex.Message);
			}



			#endregion

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
