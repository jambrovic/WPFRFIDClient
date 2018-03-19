using RFIDClient.Data;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Service
{
    /// <summary>
    /// Middle application layer - business logic
    /// <para/>User repository service class for CRUD operations
    /// </summary>
    public class UserRepositoryService : IRepositoryService<UserService>
    {
        public async Task<long> Delete(string id)
        {
            return await UserFactory.Instance.DeleteAsync(id);
        }

        public async Task Insert(UserService entity)
        {
            await UserFactory.Instance.InsertAsync(EntityConverter.GetUser(entity));
        }

        public async Task<UserService> Select(string id)
        {
            return EntityConverter.GetUser(await UserFactory.Instance.SelectAsync(id));
        }

        public async Task<bool> CheckPassword(string username)
        {
            await Task.Delay(1000);
            return true;
        }

        public async Task<List<UserService>> SelectAll()
        {
            return EntityConverter.GetUsers(await UserFactory.Instance.SelectAllAsync());
        }

        public async Task<long> Update(UserService entity)
        {
            return await UserFactory.Instance.UpdateAsync(EntityConverter.GetUser(entity));
        }
    }
}
