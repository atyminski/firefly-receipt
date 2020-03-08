using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Gevlee.FireflyReceipt.Application.Views
{
    public class ReceiptsSearchSettingsView : UserControl
    {
        public ReceiptsSearchSettingsView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
