using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Services
{
    public interface ICacheService
    {
        Task<T?> GetAll<T>(string key);
        Task SetAsync<T>(string key, T value, int minutesToLive = 10);
        Task RemoveAsync(string key);

    }
}
