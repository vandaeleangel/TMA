using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TMA.Mobile.Domain.Services.Interfaces
{
    public interface IHttpConnector
    {
        Task<T> GetApiResult<T>(string uri);
        Task<TOut> PutCallApi<TOut, TIn>(string uri, TIn entity);
        Task<TOut> PostCallApi<TOut, TIn>(string uri, TIn entity);
        Task<TOut> DeleteCallApi<TOut>(string uri);
        Task<TOut> CallApi<TOut, TIn>(string uri, TIn entity, HttpMethod httpMethod);
    }
}
