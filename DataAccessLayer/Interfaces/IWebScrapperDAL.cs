using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IWebScrapperDAL
    {
        public Task<DataResponse<string>> ScrapeInstagramWithDefaultAccount(bool headless, string profile);
    }
}
