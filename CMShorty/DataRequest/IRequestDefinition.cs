namespace DataRequest
{
    using System;
    using DataRequest.Models;

    public interface IRequestDefinition<TSuccssResult, TFailResult, TParameter>
    {
        string EndpointURL { get; }

        event EventHandler<RequestEventArgs<TSuccssResult>> Success;

        event EventHandler<RequestEventArgs<TFailResult>> Fail;

        void Process(TParameter parameter);
    }
}
