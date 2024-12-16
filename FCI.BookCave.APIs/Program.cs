using FCI.BookCave.Application;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Infrastructure;
using FCI.BookCave.Persistence;
using FCI.BookCave.Persistence.Data;
using FCI.BookCave.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

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
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					BearerFormat = "JWT"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			});
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("Default", config => config.AllowAnyMethod().WithOrigins("http://localhost:3000").AllowAnyHeader().AllowCredentials());
			});
			builder.Services.AddInfrastructureServices(builder.Configuration);
			builder.Services.AddPersistenceServices(builder.Configuration);
			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.SignIn.RequireConfirmedEmail = true;


			}).AddEntityFrameworkStores<IdentityDbContext>();

			var app = builder.Build();

			#region InitalizeDb
			
			var scope = app.Services.CreateScope();

			var identityDbInitializer = scope.ServiceProvider.GetRequiredService<IdentityDbContextInitializer>();
			var storeDbInitializer = scope.ServiceProvider.GetRequiredService<StoreDbContextInitialzer>();

			try
			{
				await storeDbInitializer.InitializeAsync();
				await identityDbInitializer.InitializeAsync();
				await storeDbInitializer.SeedAsync();
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

			app.UseCors("Default");
			app.UseAuthorization();

			app.UseStaticFiles();

			app.UseAuthentication();

			app.MapControllers();

			app.Run();
		}
	}
}
