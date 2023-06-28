namespace CMShorty.URLShortModul.Manager
{
    using CMShorty.URLShortModul.Models;
    using DataRequest.Models;
    using System;
    using System.Collections.Generic;

    public interface IShortUrlManager
    {
        void RequestShortUrl(string longUrl);

        event EventHandler<RequestEventArgs<ShortURLModel>> ShortUrlCreated;

        event EventHandler<RequestEventArgs<Exception>> ShortUrlFailed;

        List<ShortURLModel> ShortURLCollection { get; }
    }
}
