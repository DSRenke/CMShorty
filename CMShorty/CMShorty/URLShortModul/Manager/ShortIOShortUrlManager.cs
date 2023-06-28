namespace CMShorty.URLShortModul.Manager
{
    using System;
    using System.Collections.Generic;
    using CMShorty.URLShortModul.Mappers;
    using CMShorty.URLShortModul.Models;
    using DataRequest.Models;
    using Flurl.Http;
    using ShortyIODataRequest.CreateShortURL;

    public class ShortIOShortUrlManager : IShortUrlManager
    {
        private const string SetttingsKeyShortHistorie = "SetttingsKeyShortHistorie";

        private CreateShortURLRequest request;

        private List<ShortURLModel> shortURLCollection;

        private readonly ISettingsManager settingsManager;

        public ShortIOShortUrlManager(ISettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;
            this.shortURLCollection = this.settingsManager.Get<List<ShortURLModel>>(SetttingsKeyShortHistorie) ?? new List<ShortURLModel>();
        }

        public List<ShortURLModel> ShortURLCollection => this.shortURLCollection;

        public event EventHandler<RequestEventArgs<ShortURLModel>> ShortUrlCreated;

        public event EventHandler<RequestEventArgs<Exception>> ShortUrlFailed;

        public void RequestShortUrl(string longUrl)
        {
            if (request == null)
            {
                request = new CreateShortURLRequest();
                request.Success += this.RequestSuccess;
                request.Fail += this.RequestFail;
            }

            var parameter = new ShortRequestParameter();
            parameter.OriginalURL = longUrl;
            parameter.Domain = "a64a.short.gy";
            parameter.AllowDuplicates = false;
            parameter.RedirectType = 302;
            parameter.Authorization = "key...";

            request.Process(parameter);
        }

        private void RequestFail(object sender, RequestEventArgs<FlurlHttpException> e)
        {
            this.ShortUrlFailed?.Invoke(this, new RequestEventArgs<Exception>(e.RequestResult));
        }

        private void RequestSuccess(object sender, RequestEventArgs<ShortRequstSuccessResult> e)
        {
            var mapper = new ShortURLMapper();
            var uiShortModel = mapper.Map(e.RequestResult);

            this.shortURLCollection.Add(uiShortModel);

            this.settingsManager.Set(SetttingsKeyShortHistorie, this.shortURLCollection);

            this.shortURLCollection = this.settingsManager.Get<List<ShortURLModel>>(SetttingsKeyShortHistorie) ?? new List<ShortURLModel>();

            this.ShortUrlCreated?.Invoke(this, new RequestEventArgs<ShortURLModel>(uiShortModel));
        }
    }
}