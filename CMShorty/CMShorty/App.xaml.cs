namespace CMShorty
{
    using CMShorty.Navigation;
    using CMShorty.URLShortModul.Manager;
    using CMShorty.URLShortModul.Views;
    using Prism.Ioc;
    using Prism.Unity;
    using Xamarin.Forms;

    public partial class App : PrismApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnInitialized()
        {
            this.MainPage = new MainShell();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Routing.RegisterRoute(nameof(Overview), typeof(Overview));
            containerRegistry.RegisterSingleton<ISettingsManager, PreferencSettingsManager>();
            containerRegistry.RegisterSingleton<IShortUrlManager, ShortIOShortUrlManager>();
            containerRegistry.RegisterSingleton<AppConfigManager>();
        }
    }
}
