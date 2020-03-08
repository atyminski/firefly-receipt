using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace Gevlee.FireflyReceipt.Application.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            LoadImages();
        }

        public ReceiptsSearchSettingsViewModel ReceiptsSearchSettingsModel { get; } = new ReceiptsSearchSettingsViewModel();

        public ReceiptsBrowserViewModel ReceiptsBrowserModel { get; } = new ReceiptsBrowserViewModel();

        public TransactionsListViewModel TransactionsListModel { get; } = new TransactionsListViewModel();

        public void LoadImages()
        {
            var filterRegex = new Regex(ReceiptsSearchSettingsModel.FilterRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            ReceiptsBrowserModel.ReceiptsPaths = Directory.EnumerateFiles(ReceiptsSearchSettingsModel.ReceiptsDir)
                .Where(x => filterRegex.IsMatch(Path.GetFileName(x)))
                .OrderByDescending(x => new FileInfo(x).CreationTimeUtc);
        }
    }
}
