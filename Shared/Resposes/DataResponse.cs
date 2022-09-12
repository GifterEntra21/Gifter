using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Resposes
{
    public class DataResponse<T>
    {
        public bool HasSucces { get; set; }
        public string Message { get; set; }
        public List<T> Item { get; set; }
    }
}
