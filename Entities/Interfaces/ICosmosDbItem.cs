using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface ICosmosDbItem
    {
        public string id { get; set; }
        public string PartitionKey { get; }
    }
}
