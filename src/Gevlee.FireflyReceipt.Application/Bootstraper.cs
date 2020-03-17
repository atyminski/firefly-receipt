using Gevlee.FireflyReceipt.Application.Services;
using Gevlee.FireflyReceipt.Application.Settings;
using Gevlee.FireflyReceipt.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Serilog;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using Splat.Microsoft.Extensions.Logging;
using System;

namespace Gevlee.FireflyReceipt.Application
{
    public static class Bootstraper
    {
        public static IServiceProvider Init()
        {
            var host = Host
              .CreateDefaultBuilder()
              .UseSerilog()
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
            services.AddTransient<IAttachmentService, AttachmentService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IFireflyClient, FireflyClient>();
        }
    }
}