using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    /// <summary>
    /// Generic interface for CRUD operations on Mongo collections
    /// </summary>
    /// <typeparam name="T">Mongo collection class</typeparam>
    public interface IRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        Task<T> SelectAsync(string id);
        Task<List<T>> SelectAllAsync();
        Task<long> UpdateAsync(T entity);
        Task<long> DeleteAsync(string id);

    }
}
