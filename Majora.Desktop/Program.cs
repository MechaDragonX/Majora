using Chromely.Core;

namespace Majora.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            // basic example of the application builder
            AppBuilder
            .Create()
            .UseApp<Majora>()
            .Build()
            .Run(args);
        }
    }
}
