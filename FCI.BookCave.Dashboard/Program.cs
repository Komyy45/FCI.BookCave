using FCI.BookCave.Abstractions.Contracts;
using FCI.BookCave.Abstractions.Models.Common;
using FCI.BookCave.Application.Mapping;
using FCI.BookCave.Dashboard.Models;
using FCI.BookCave.Dashboard.UploadImage;
using FCI.BookCave.Domain.Contracts.GenericRepository;
using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Persistence.Data;
using FCI.BookCave.Persistence.Identity;
using FCI.BookCave.Persistence.Repositories;
using FCI.BookCave.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FCI.BookCave.Application.Services.Identity;
using FCI.BookCave.Dashboard.Email;
using FCI.BookCave.Dashboard.Models.Adminstration;
using Microsoft.Extensions.DependencyInjection;

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

            builder.Services.AddDbContext<IdentityDbContext>(opt => opt.UseSqlServer(
                builder.Configuration.GetConnectionString("IdentityConnection")
                )
            );

            builder.Services.AddScoped(typeof(DbContext),typeof(StoreDbContext));
            builder.Services.AddScoped<IFileUpload, FileUpload>();
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            builder.Services.AddTransient<IEmailService, EmailService>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<IdentityDbContext>();

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
