using Library.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Library.Services;
using System.IO;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Library.Data.Models;
using Library.Data.Entities.Auth;
using Library.Services.Email;
using Library.Services.Auth;

namespace SportTeams
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			#region dbContexts

			services.AddDbContext<DatabaseContext>(options =>
			   options.UseSqlServer(Configuration.GetConnectionString("SQLDatabase")));
			//services.AddScoped<DatabaseContext>(sp => sp.GetRequiredService<DatabaseContext>());

			services.AddDbContext<AuthentificationContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("AuthDatabase")));
			services.AddScoped<IAuthenticationService, JwtAuthenticationService>();

			//services.AddScoped<ICacheManager, MemoryCacheManager>();
			services.AddScoped(typeof(DataRepository<>), typeof(DataRepository<>));

			#endregion

			#region Auth
			services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
			services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

			services.AddIdentity<ApplicationUser, IdentityRole<Int32>>()
				.AddEntityFrameworkStores<AuthentificationContext>()
				.AddRoles<IdentityRole<Int32>>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(opt =>
			{
				opt.Password.RequireNonAlphanumeric = false;
				opt.SignIn.RequireConfirmedEmail = false;
				opt.Password.RequireDigit = false;
				opt.Password.RequiredLength = 8;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireUppercase = false;
			});
			services.AddCors();

			services.Configure<DataProtectionTokenProviderOptions>(o =>
				o.TokenLifespan = TimeSpan.FromDays(7));

			//Jwt Authentification
			var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = false;
				x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero

				};
			});
			#endregion

			#region Services
			services.AddScoped<PersonService, PersonService>();
			services.AddScoped<TeamService, TeamService>();
			services.AddScoped<MemberService, MemberService>();



			services.AddScoped<IEmailService, EmailService>();


			#endregion

			services.AddControllersWithViews();
			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			app.UseRouting();

			//app.Use(async (context, next) =>
			//{
			//	await next();
			//	if (context.Response.StatusCode == 404 &&
			//	!Path.HasExtension(context.Request.Path.Value) &&
			//	!context.Request.Path.Value.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
			//	{
			//		context.Request.Path = "/index.html";
			//		await next();
			//	}
			//});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});


		}
	}
}
