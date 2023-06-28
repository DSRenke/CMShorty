namespace CMShorty.URLShortModul.Views
{
    using Prism.Mvvm;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Overview : ContentPage
    {
        public Overview()
        {
            ViewModelLocator.SetAutowireViewModel(this, true);
            this.InitializeComponent();
        }
    }
}