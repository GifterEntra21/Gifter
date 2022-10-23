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
        /// <summary>
        /// Caso a aplicação esteja em desenvolvimento, uma pagina do chromium será aberta para executar o WebScrapping, 
        /// caso esteja em produção, será executada remotamente no Browserless.io
        /// </summary>
        /// <param name="headless"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        public Task<DataResponse<string>> ScrapeInstagramWithDefaultAccount(bool headless, string profile);
    }
}
