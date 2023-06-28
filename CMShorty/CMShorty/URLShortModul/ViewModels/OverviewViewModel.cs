namespace CMShorty.URLShortModul.ViewModels
{
    using System;
    using System.Collections.Generic;
    using CMShorty.URLShortModul.Manager;
    using CMShorty.URLShortModul.Models;
    using DataRequest.Models;
    using Prism.Mvvm;
    using Xamarin.Forms;

    public class OverviewViewModel : BindableBase
    {
        private readonly IShortUrlManager shortUrlManager;
        private Command shortCommand;
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
            this.shortUrlManager.RequestShortUrl(this.EntryURL);
        }

        private void RequestFail(object sender, RequestEventArgs<Exception> e)
        {
            Shell.Current.DisplayAlert("Fehler", e.RequestResult.Message, "Ok");
        }

        private void RequestSuccess(object sender, RequestEventArgs<ShortURLModel> e)
        {
            this.DataSource = this.shortUrlManager.ShortURLCollection;
        }
    }
}
