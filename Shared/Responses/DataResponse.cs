using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class DataResponse<T>
    {
        public DataResponse(string message, bool hasSucces, List<T> item, Exception exception)
        {
            Message = message;
            HasSucces = hasSucces;
            Item = item;
            Exception = exception;
        }

        public string Message { get; set; }
        public bool HasSucces { get; set; }
        public List<T> Item { get; set; }
        public Exception Exception { get; set; }

    }
}
