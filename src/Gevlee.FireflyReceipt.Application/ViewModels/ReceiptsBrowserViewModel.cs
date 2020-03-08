using Avalonia.Controls.Shapes;
using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Gevlee.FireflyReceipt.Application.ViewModels
{
    public class ReceiptsBrowserViewModel : ViewModelBase, IActivatableViewModel
    {
        private IEnumerable<string> receiptsPaths;
        private string selectedReciptPath;
        private int selectedReciptPathIndex;
        private IBitmap recepitImg;

        public ReceiptsBrowserViewModel()
        {
            this.WhenAnyValue(x => x.SelectedReciptPath)
                .Where(x => !string.IsNullOrWhiteSpace(x) && File.Exists(x))
                .Subscribe(SetImage);
        }

        public IEnumerable<string> ReceiptsPaths { get => receiptsPaths; set => this.RaiseAndSetIfChanged(ref receiptsPaths, value); }

        public string SelectedReciptPath { get => selectedReciptPath; set => this.RaiseAndSetIfChanged(ref selectedReciptPath, value); }

        public int SelectedReciptPathIndex { get => selectedReciptPathIndex; set => this.RaiseAndSetIfChanged(ref selectedReciptPathIndex, value); }

        public IBitmap ReceiptImg { get => recepitImg; set => this.RaiseAndSetIfChanged(ref recepitImg, value); }

        public ReactiveCommand<Unit, Unit> OnNext => ReactiveCommand.Create(NextImg, this.WhenAnyValue(x => x.SelectedReciptPathIndex).Select(x => x < ReceiptsPaths.Count() - 1));

        public ReactiveCommand<Unit, Unit> OnPrevious => ReactiveCommand.Create(PreviousImg, this.WhenAnyValue(x => x.SelectedReciptPathIndex).Select(x => x > 0));

        private void PreviousImg()
        {
            SelectedReciptPathIndex--;
        }

        private void NextImg()
        {
            SelectedReciptPathIndex++;
        }

        public ViewModelActivator Activator => new ViewModelActivator();

        private void SetImage(string imgPath)
        {
            ReceiptImg = new Bitmap(imgPath);
        }
    }
}
