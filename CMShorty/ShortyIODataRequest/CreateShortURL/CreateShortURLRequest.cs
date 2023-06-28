namespace ShortyIODataRequest.CreateShortURL
{
    using System;
    using System.Net.Http.Headers;
    using System.Net.Http;
    using DataRequest;
    using DataRequest.Models;
    using Flurl.Http;
    using Newtonsoft.Json;

    public class CreateShortURLRequest : IRequestDefinition<ShortRequstSuccessResult, FlurlHttpException, ShortRequestParameter>
    {
        public string EndpointURL => "https://api.short.io/links";

        public event EventHandler<RequestEventArgs<ShortRequstSuccessResult>> Success;

        public event EventHandler<RequestEventArgs<FlurlHttpException>> Fail;

        public async void Process(ShortRequestParameter parameter)
        {
            try
            {
                string json = JsonConvert.SerializeObject(parameter);
                StringContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var createdPost = await this.EndpointURL
                    .WithHeader("accept", "application/json")
                    .WithHeader("content-type", "application/json")
                    .WithHeader("Authorization", parameter.Authorization)
                    .PostAsync(content)
                    .ReceiveJson<ShortRequstSuccessResult>();
                this.Success?.Invoke(this, new RequestEventArgs<ShortRequstSuccessResult>(createdPost));
            }
            catch (FlurlHttpException ex)
            {
                // Fehlerbehandlung für HTTP-Fehler
                Console.WriteLine($"HTTP-Fehler: {ex.Message}");
                this.Fail?.Invoke(this, new RequestEventArgs<FlurlHttpException>(new FlurlHttpException(ex.Call, ex)));
            }
        }
    }
}