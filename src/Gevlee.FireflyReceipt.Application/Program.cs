using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using Serilog;

[assembly: PropertyChanged.FilterType("Gevlee.FireflyReceipt.Application.Models")]
namespace Gevlee.FireflyReceipt.Application
{
    public class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    //.MinimumLevel.Verbose()
            //    //.MinimumLevel.Override("Avalonia", Serilog.Events.LogEventLevel.Verbose)
            //    .WriteTo.File("firefly-receipts.log")
            //    .WriteTo.Debug()
            //    .CreateLogger();

            //SerilogLogger.Initialize(Log.Logger);

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            var builder = AppBuilder.Configure<App>()
                //.LogToDebug(Avalonia.Logging.LogEventLevel.Debug)
                .UsePlatformDetect()
                .UseReactiveUI();
            var runtime = builder.RuntimePlatform.GetRuntimeInfo();

            switch (runtime.OperatingSystem)
            {
                case OperatingSystemType.OSX:
                    builder.UseAvaloniaNative()
                        .With(new AvaloniaNativePlatformOptions
                        {
                            UseGpu = true,
                            UseDeferredRendering = true
                        })
                        .UseSkia();
                    break;

                case OperatingSystemType.Linux:
                    builder.UseX11()
                        .With(new X11PlatformOptions
                        {
                            UseGpu = true
                        })
                        .UseSkia();
                    break;

                default:
                    builder.UseWin32()
                        .With(new Win32PlatformOptions
                        {
                            UseDeferredRendering = true
                        })
                        .UseSkia();
                    break;
            }

            return builder;
        }
    }
}
