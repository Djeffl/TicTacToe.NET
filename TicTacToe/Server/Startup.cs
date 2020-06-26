using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Linq;
using TicTacToe.Server.Repositories;

namespace TicTacToe.Server
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddResponseCompression(opts =>
			{
				opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
					new[] { "application/octet-stream" });
			});

			services.AddDbContext<TicTacToeContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("TicTacToeContext")));

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
			});

			services.AddControllersWithViews(options =>
			{
				options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
			});

			services.AddTransient<Services.PlayfieldStatesService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBlazorDebugging();
			}

			app.UseStaticFiles();
			app.UseClientSideBlazorFiles<Client.Program>();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
				endpoints.MapFallbackToClientSideBlazor<Client.Program>("index.html");
			});
		}

		/// <summary>
		/// add support for JSON Patch
		/// </summary>
		/// <returns></returns>
		/// https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-3.1
		private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
		{
			var builder = new ServiceCollection()
				.AddLogging()
				.AddMvc()
				.AddNewtonsoftJson()
				.Services.BuildServiceProvider();

			return builder
				.GetRequiredService<IOptions<MvcOptions>>()
				.Value
				.InputFormatters
				.OfType<NewtonsoftJsonPatchInputFormatter>()
				.First();
		}
	}
}
