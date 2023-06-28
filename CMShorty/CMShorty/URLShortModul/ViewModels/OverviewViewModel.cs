namespace CMShorty.URLShortModul.ViewModels
{
    using Prism.Mvvm;
    using Xamarin.Forms;

    public class OverviewViewModel : BindableBase
    {
        private Command shortCommand;
        private string entryURL;

        public OverviewViewModel()
        {
        }

        public Command ShortCommand => shortCommand ?? (shortCommand = new Command(this.ExecuteShortCommand));

        public string EntryURL
        {
            get => this.entryURL;
            set => this.SetProperty(ref this.entryURL, value);
        }

        private void ExecuteShortCommand(object obj)
        {
            // URL Validieren
            // URL kurzen
        }
    }
}
