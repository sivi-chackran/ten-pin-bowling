using Newtonsoft.Json;
using System.IO;
using TenPinBowling.Common.Model.Config;

namespace TenPinBowling.Tests
{
    public class TestBase
    {

        protected readonly AppSettings appSettings;
        public TestBase()
        {
            appSettings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText("appsettings.json"));
        }
    }
}
