using FCI.BookCave.Dashboard.Models;
using FCI.BookCave.Dashboard.UploadImage;
using FCI.BookCave.Domain.Contracts.GenericRepository;
using FCI.BookCave.Persistence.Data;
using FCI.BookCave.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
namespace FCI.BookCave.Dashboard
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoreDbContext>(opt => opt.UseSqlServer(
				builder.Configuration.GetConnectionString("DefaultConnection")
				)
			);
			builder.Services.AddScoped(typeof(DbContext),typeof(StoreDbContext));
            builder.Services.AddScoped<IFileUpload, FileUpload>();
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));


            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
