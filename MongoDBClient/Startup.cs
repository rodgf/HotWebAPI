using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDBClient.Database;

namespace MongoDBClient {

  //
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddRazorPages();
      services.AddMvc();
      services.AddControllers(options => options.EnableEndpointRouting = false);

      services.AddScoped<DBHpr>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      } else {
        app.UseExceptionHandler("/Error");
      }

      app.UseStaticFiles();

      app.UseRouting();

      app.UseMvc(routes => {
        routes.MapRoute(
            name: "default",
            template: "{controller=Graphql}/{action=Index}/{id?}");
      });
    }
  }
}
