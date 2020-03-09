using Avalonia.Media.Imaging;
using Gevlee.FireflyReceipt.Application.Models;
using Gevlee.FireflyReceipt.Application.Services;
using Gevlee.FireflyReceipt.Application.Settings;
using Microsoft.Extensions.Options;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gevlee.FireflyReceipt.Application.ViewModels
{
    public class ReceiptsBrowserViewModel : ViewModelBase, IActivatableViewModel
    {
        private IEnumerable<ReceiptsListItem> receiptsPaths;
        private ReceiptsListItem selectedReciptPath;
        private int selectedReciptPathIndex;
        private IBitmap recepitImg;
        private IAttachmentService attachmentService;

        public ReceiptsBrowserViewModel()
        {
            attachmentService = Locator.Current.GetService<IAttachmentService>();
            this.WhenAnyValue(x => x.SelectedRecipt)
                .Where(x => x != null && !string.IsNullOrWhiteSpace(x.Path) && File.Exists(x.Path))
                .Select(x => x.Path)
                .Subscribe(SetImage);

            Receipts = new List<ReceiptsListItem>();

            _ = LoadImagesAsync();
        }

        public IEnumerable<ReceiptsListItem> Receipts { get => receiptsPaths; set => this.RaiseAndSetIfChanged(ref receiptsPaths, value); }

        public ReceiptsListItem SelectedRecipt { get => selectedReciptPath; set => this.RaiseAndSetIfChanged(ref selectedReciptPath, value); }

        public int SelectedReciptIndex { get => selectedReciptPathIndex; set => this.RaiseAndSetIfChanged(ref selectedReciptPathIndex, value); }

        public IBitmap ReceiptImg { get => recepitImg; set => this.RaiseAndSetIfChanged(ref recepitImg, value); }

        public ReactiveCommand<Unit, Unit> OnNext => ReactiveCommand.Create(NextImg, this.WhenAnyValue(x => x.SelectedReciptIndex, x => x.Receipts).Select(x => x.Item2.Any() && x.Item1 < Receipts.Count() - 1));

        public ReactiveCommand<Unit, Unit> OnPrevious => ReactiveCommand.Create(PreviousImg, this.WhenAnyValue(x => x.SelectedReciptIndex, x => x.Receipts).Select(x => x.Item2.Any() && x.Item1 > 0));

        private void PreviousImg()
        {
            SelectedReciptIndex--;
        }

        private void NextImg()
        {
            SelectedReciptIndex++;
        }

        public ViewModelActivator Activator => new ViewModelActivator();

        private void SetImage(string imgPath)
        {
            ReceiptImg = new Bitmap(imgPath);
        }

        public async Task LoadImagesAsync()
        {
            var alreadyAssigned = await attachmentService.GetAlreadyAssignedReceipts();
            var generalSettings = Locator.Current.GetService<IOptions<GeneralSettings>>().Value;
            var filterRegex = new Regex(generalSettings.FilterRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Receipts = Directory.EnumerateFiles(generalSettings.ReceiptsDir)
                .Where(x => filterRegex.IsMatch(Path.GetFileName(x)))
                .OrderByDescending(x => new FileInfo(x).CreationTimeUtc)
                .Select(x => new ReceiptsListItem
                {
                    Path = x,
                    IsAlreadyAssigned = alreadyAssigned
                        .Any(y => y.Filename.Equals(Path.GetFileName(x), StringComparison.OrdinalIgnoreCase))
                });
        }
    }
}
