namespace ShortyIODataRequest.CreateShortURL
{
    using System;
    using System.Collections.Generic;

    public class ShortRequstSuccessResult
    {
        public string originalURL { get; set; }

        public int DomainId { get; set; }

        public bool archived { get; set; }

        public string source { get; set; }

        public bool cloaking { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public int OwnerId { get; set; }

        public List<string> tags { get; set; }

        public string path { get; set; }

        public string idString { get; set; }

        public string redirectType { get; set; }

        public string shortURL { get; set; }

        public string secureShortURL { get; set; }

        public bool duplicate { get; set; }
    }
}
