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

            public Response CreateSuccessResponse()
            {
                return new Response("Operação realizada com sucesso.", true, null);
            }
            public SingleResponse<T> CreateSingleSuccessResponse<T>(T item)
            {
                return new SingleResponse<T>("Dado coletado com sucesso", true, item, null);
            }
            public DataResponse<T> CreateDataSuccessResponse<T>(List<T> item)
            {
                return new DataResponse<T>("Dados coletados com sucesso", true, item, null);
            }



            public Response CreateFailedResponse(Exception ex)
            {
                return new Response("Erro no banco de dados, contate o administrador.", false, ex);
            }
            public Response CreateFailedResponse(Exception ex, string message)
            {
                return new Response(message, false, ex);
            }
            public Response CreateNotFoundIdResponse()
            {
                return new Response("Not found ID.", false, null);
            }



            public SingleResponse<T> CreateSingleNotFoundIdResponse<T>(T item)
            {
                return new SingleResponse<T>("Not found ID on database.", false, item, null);
            }
            public SingleResponse<T> CreateSingleFailedResponse<T>(Exception ex, T item)
            {
                return new SingleResponse<T>("Erro no banco, contate o administrador.", false, item, ex);
            }
            public SingleResponse<T> CreateSingleFailedResponse<T>(Exception ex, T item, string message)
            {
                return new SingleResponse<T>(message, false, item, ex);
            }



            public DataResponse<T> CreateDataFailedResponse<T>(Exception ex)
            {
                return new DataResponse<T>("Erro no banco, contate o administrador.", false, null, ex);
            }
        }
    }
