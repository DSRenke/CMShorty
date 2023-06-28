namespace CMShorty.URLShortModul.Models
{
    using System;

    public class ShortURLModel
    {
        public string OriginalURL { get; set; }

        public string ShortURL { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
