using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class Response
    {
        public Response(string message, bool hasSucess, Exception exception)
        {
            Message = message;
            HasSucess = hasSucess;
            Exception = exception;
        }

        public string Message { get; set; }
        public bool HasSucess { get; set; }
        public Exception Exception {get;set;}
    }
}
