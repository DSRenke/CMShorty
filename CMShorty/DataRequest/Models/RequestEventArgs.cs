using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRequest.Models
{
    public class RequestEventArgs<TResult> : EventArgs
    {
        public RequestEventArgs(TResult requestResult)
        {
            this.RequestResult = requestResult;
        }

        public TResult RequestResult { get; set; }
    }
}
