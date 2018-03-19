using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Service
{
    public interface IRepositoryService<T> where T : class
    {
        Task Insert(T entity);
        Task<T> Select(string id);
        Task<List<T>> SelectAll();
        Task<long> Update(T entity);
        Task<long> Delete(string id);
    }
}
