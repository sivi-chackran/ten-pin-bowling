using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TenPinBowling.Common.Model.Config;

namespace TenPinBowling.Api.Extensions
{
    public static class SettingsConfiguartionExtensions
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton(Configuration);
            services.Configure<AppSettings>(options => Configuration.Bind(options));

            var options = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();
            services.AddSingleton(options.Value);

            return services;
        }
    }
}
