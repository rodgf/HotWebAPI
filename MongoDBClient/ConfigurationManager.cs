using Microsoft.Extensions.Configuration;
using System.IO;

namespace CSHelper {

  //
  static class ConfigurationManager {
    public static IConfiguration AppSetting { get; set; }

    static ConfigurationManager() {
      AppSetting = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
    }
  }
}
