using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class DataResponse<T>
    {
        public DataResponse(string message, bool hasSuccess, List<T> itemList, Exception exception)
        {
            Message = message;
            HasSuccess = hasSuccess;
            ItemList = itemList;
            Exception = exception;
        }

        public string Message { get; set; }
        public bool HasSuccess { get; set; }
        public List<T> ItemList { get; set; }
        public Exception Exception { get; set; }

    }
}
