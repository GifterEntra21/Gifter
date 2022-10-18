using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public class ResponseFactory
    {
        #region Singleton
        private static ResponseFactory _factory;

        public static ResponseFactory CreateInstance()
        {
            if (_factory == null)
            {
                _factory = new ResponseFactory();
            }
            return _factory;
        }

        private ResponseFactory() { }
        #endregion Singleton

        //success Responses, SingleResponses and DataResponses

        public Response CreateSuccessResponse()
        {
            return new Response("Operação realizada com sucesso.", true, null);
        }
        public SingleResponse<T> CreateSuccessSingleResponse<T>(T item)
        {
            return new SingleResponse<T>("Dado coletado com sucesso", true, item, null);
        }
        public DataResponse<T> CreateSuccessDataResponse<T>(List<T> item)
        {
            return new DataResponse<T>("Dados coletados com sucesso", true, item, null);
        }

        //failed Responses

        public Response CreateFailedResponse(Exception ex)
        {
            return new Response("Erro no banco de dados, contate o administrador.", false, ex);
        }
        public Response CreateFailedResponse(Exception ex, string message)
        {
            return new Response(message, false, ex);
        }


        //failed SingleResponses


        public SingleResponse<T> CreateFailedSingleResponse<T>(Exception ex)
        {
            return new SingleResponse<T>("Erro no banco, contate o administrador.", false, ex);
        }
        public SingleResponse<T> CreateFailedSingleResponse<T>(Exception ex, string message)
        {
            return new SingleResponse<T>(message, false, ex);
        }

        //failed DataResponses

        public DataResponse<T> CreateFailedDataResponse<T>(Exception ex)
        {
            return new DataResponse<T>("Erro no banco, contate o administrador.", false, null, ex);
        }

        public DataResponse<T> CreateFailedDataResponse<T>(Exception ex, string message)
        {
            return new DataResponse<T>(message, false, null, ex);
        }
    }
}
