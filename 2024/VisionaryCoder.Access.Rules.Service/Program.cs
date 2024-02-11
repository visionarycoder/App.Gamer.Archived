using Microsoft.Extensions.Hosting;

namespace VisionaryCoder.Access.Rules.Service;
public class Hosting
{
    public async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureDefaults(args)
            .ConfigureLogging(builder => { })
            .ConfigureAppConfiguration((context, builder) => { })
            .ConfigureServices((context, services) => { })
            .Build();
        await host.StartAsync();
    }
}