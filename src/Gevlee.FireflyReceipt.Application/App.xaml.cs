using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Gevlee.FireflyReceipt.Application.ViewModels;
using Gevlee.FireflyReceipt.Application.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gevlee.FireflyReceipt.Application
{
    public class App : Avalonia.Application
    {
        public App()
        {
            Container = Bootstraper.Init();
        }

        public IServiceProvider Container { get; set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = Container.GetRequiredService<MainWindowViewModel>(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
