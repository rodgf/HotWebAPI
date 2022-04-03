using System;
using System.IO;
using CSHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MongoDBClient {

  //
  public class Program {
    public static void Main(string[] args) {
      CreateHostBuilder(args).Build().Run();
    }

    //
    public static IHostBuilder CreateHostBuilder(string[] args) {
      IHostBuilder result = Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<Startup>();
          });

      ConfigurationManager.AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
                .Build();

      return result;
    }
  }
}
