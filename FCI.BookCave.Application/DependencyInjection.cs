using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FCI.BookCave.Abstractions.Contracts;
using FCI.BookCave.Abstractions.Contracts.Products;
using FCI.BookCave.Abstractions.Models.Common;
using FCI.BookCave.Application.Mapping;
using FCI.BookCave.Application.Services.Identity;
using FCI.BookCave.Application.Services.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FCI.BookCave.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configurations)
		{
			#region JWT

			services.AddAuthentication(options =>
			{
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ClockSkew = TimeSpan.FromMinutes(0),
					ValidIssuer = configurations["JwtSettings:Issuer"],
					ValidAudience = configurations["JwtSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurations["JwtSettings:Key"]!))
				};
			}
			);

			#endregion

			services.AddScoped<IAuthService, AuthService>();
			services.Configure<JwtSettings>(configurations.GetSection("JwtSettings"));

			services.AddScoped<ImageUrlResolver>();
			services.AddScoped<MapperlyMapper>();

			services.AddScoped<IProductService, ProductService>();


			return services;
		}

	}
}
