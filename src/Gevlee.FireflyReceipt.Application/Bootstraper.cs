using Gevlee.FireflyReceipt.Application.Settings;
using Gevlee.FireflyReceipt.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using Splat.Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Gevlee.FireflyReceipt.Application
{
    public static class Bootstraper
    {
        public static IServiceProvider Init()
        {
            var host = Host
              .CreateDefaultBuilder()
              .ConfigureServices((context, services) =>
              {
                  services.UseMicrosoftDependencyResolver();
                  var resolver = Locator.CurrentMutable;
                  resolver.InitializeSplat();
                  resolver.InitializeReactiveUI();

                  ConfigureServices(context, services);
              })
              .ConfigureAppConfiguration(builder =>
              {
                  //builder.SetBasePath(Directory.GetCurrentDirectory())
                  //  .AddJsonFile("config.json")
                  //  .AddJsonFile("config.dev.json", true);
              })
              .ConfigureLogging(loggingBuilder =>
              {
                  loggingBuilder.AddSplat();
              })
#if DEBUG
              .UseEnvironment(Environments.Development)
#endif
              .Build();

            return host.Services;
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.Configure<GeneralSettings>(context.Configuration);
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<ReceiptsBrowserViewModel>();
            services.AddTransient<ReceiptsSearchSettingsViewModel>();
        }
    }
}