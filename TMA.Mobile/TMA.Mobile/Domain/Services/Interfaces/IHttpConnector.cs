using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TMA.Mobile.Domain.Services.Interfaces
{
    public interface IHttpConnector
    {
        Task<T> GetApiResult<T>(string uri);
    }
}
