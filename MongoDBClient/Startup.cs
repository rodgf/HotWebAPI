using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDBClient.Database;
using MongoDBClient.GraphQL;

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

      services.AddTransient<DBHpr>();

      // GraphQL
      services
          .AddGraphQLServer()
          .AddQueryType(d => d.Name("Query"))
              .AddTypeExtension<NoteQuery>()
              .AddTypeExtension<AlunoQuery>()
          .AddType<NoteType>()
          .AddType<AlunoType>()
          .AddType<ProjectResolver>()
          .AddType<AlunoResolver>();
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
      app.UseStatusCodePages();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
        endpoints.MapGraphQL("/api/graphql");
      });

      app.UseMvc(routes => {
        routes.MapRoute(
            name: "default",
            template: "{controller}/{action=Index}/{id?}");
      });
    }
  }
}
