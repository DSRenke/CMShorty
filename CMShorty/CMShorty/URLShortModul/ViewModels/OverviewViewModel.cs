namespace CMShorty.URLShortModul.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using CMShorty.URLShortModul.Manager;
    using CMShorty.URLShortModul.Models;
    using DataRequest.Models;
    using Prism.Mvvm;
    using Xamarin.Forms;

    public class OverviewViewModel : BindableBase
    {
        private readonly IShortUrlManager shortUrlManager;
        private Command shortCommand;
        private Command lightDarkModeCommand;
        
        private string entryURL;

        private List<ShortURLModel> dataSource;

        public OverviewViewModel(IShortUrlManager urlManager)
        {
            this.shortUrlManager = urlManager;
            this.shortUrlManager.ShortUrlCreated += this.RequestSuccess;
            this.shortUrlManager.ShortUrlFailed += this.RequestFail;
            this.DataSource = urlManager.ShortURLCollection;
        }
        
        public Command ShortCommand => shortCommand ?? (shortCommand = new Command(this.ExecuteShortCommand));

        public Command LightDarkModeCommand => lightDarkModeCommand ?? (lightDarkModeCommand = new Command(this.ExecuteLightDarkModeCommand));

        public List<ShortURLModel> DataSource
        {
            get => this.dataSource;

            set => this.SetProperty(ref this.dataSource, value);
        }

        public string EntryURL
        {
            get => this.entryURL;
            set => this.SetProperty(ref this.entryURL, value);
        }

        private void ExecuteShortCommand(object obj)
        {
            if (!this.IsUrlValide(this.EntryURL))
            {
                Shell.Current.DisplayAlert("Fehler", "Die URL ist nicht valide", "Ok");
                return;
            }

            this.shortUrlManager.RequestShortUrl(this.EntryURL);
        }

        private void ExecuteLightDarkModeCommand(object obj)
        {
            var theme = Application.Current.RequestedTheme;

            if (theme == OSAppTheme.Light)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
            }
        }

        /// <summary>
        /// Prüfe ob die URL valide ist. Falls die Url nicht mit "https://" beginnt, wird dies als Standardpräfix hinzugefügt.
        /// </summary>
        /// <param name="url"> url die geprüft werden soll.</param>
        /// <returns>True wenn url valide ist, sonst false</returns>
        private bool IsUrlValide(string url)
        {
            if (!url.StartsWith("https://"))
            {
                url = "https://" + url; // Hinzufügen von "https://" als Standardpräfix
            }

            // Quelle https://uibakery.io/regex-library/url
            string pattern = @"^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(url);
        }

        /// <summary>
        /// Callback wenn die Anfrage fehlschlägt.
        /// </summary>
        /// <param name="sender">Quelle der das event ausgelöst hat.</param>
        /// <param name="e">Fehlerbeschreibung.</param>
        private void RequestFail(object sender, RequestEventArgs<Exception> e)
        {
            Shell.Current.DisplayAlert("Fehler", e.RequestResult.Message, "Ok");
        }

        /// <summary>
        /// Callback wenn die Anfrage erfolgreich war.
        /// </summary>
        /// <param name="sender">Quelle der das event ausgelöst hat.</param>
        /// <param name="e">Resultat der Anfrage.</param>
        private void RequestSuccess(object sender, RequestEventArgs<ShortURLModel> e)
        {
            this.DataSource = this.shortUrlManager.ShortURLCollection;
        }
    }
}
