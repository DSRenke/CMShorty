namespace CMShorty.Navigation
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainShell : Shell
    {
        public MainShell()
        {
            this.InitializeComponent();
        }
    }
}