using Microsoft.Extensions.Configuration;

namespace TenPinBowling.Tests
{
    public class TestBase
    {

        public IConfigurationRoot Configuration { get; private set; }
        public TestBase()
        {
            Configuration = GetIConfigurationRoot();
        }

        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
