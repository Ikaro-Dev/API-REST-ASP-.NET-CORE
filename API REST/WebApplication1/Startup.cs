using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;


namespace WebApplication1
{
	public class Startup
	{
		#region Construtores
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		#endregion

		#region Propriedades

		public IConfiguration Configuration { get; }

		#endregion

		#region Métodos
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//Ultiliza o recurso de criação de um banco de dados em memória do Entity Framework Core. 
			services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("DatabaseInMemory"));
			services.AddScoped<DataContext, DataContext>();
			services.AddCors();
			services.AddControllers().AddNewtonsoftJson();

			var key = Encoding.ASCII.GetBytes(Settings.Secret);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(x =>
			x.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		#endregion
	}
}
