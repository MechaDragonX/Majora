using Chromely.Core;
using Chromely.Core.Configuration;
using Chromely.Core.Infrastructure;

namespace Majora
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = DefaultConfiguration.CreateForRuntimePlatform();

            config.AppName = "Majora";
            config.StartUrl = "local://App/index.html";
            config.WindowOptions.Title = "Majora";
            config.WindowOptions.RelativePathToIconFile = "local://Assets/majora.ico";
            // config.UrlSchemes.Add(new UrlScheme("custom-2", "local", string.Empty, string.Empty, UrlSchemeType.Custom, false));
#if DEBUG
            config.DebuggingMode = true;
#elif RELEASE
            config.DebuggingMode = false;
#endif

            AppBuilder
            .Create()
            .UseApp<Majora>()
            .UseConfiguration<IChromelyConfiguration>(config)
            .Build()
            .Run(args);
        }
    }
}
