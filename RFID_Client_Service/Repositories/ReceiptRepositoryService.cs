using RFIDClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Service
{
    class ReceiptRepositoryService : IRepositoryService<ReceiptService>, IAnalyticsService<ReceiptService>
    {
        public async Task<long> Delete(string id)
        {
            return await ReceiptFactory.GetInstance().DeleteAsync(id);
        }

        public async Task<List<ReceiptService>> GetPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return EntityConverter.GetReceipts(await ReceiptAnalyticsFactory.Instance.GetPeriodAsync(startDate, endDate));
        }

        public async Task Insert(ReceiptService entity)
        {
            await ReceiptFactory.GetInstance().InsertAsync(EntityConverter.GetReceipt(entity));
        }

        public async Task<ReceiptService> Select(string id)
        {
            return EntityConverter.GetReceipt(await ReceiptFactory.GetInstance().SelectAsync(id));
        }

        public async Task<List<ReceiptService>> SelectAll()
        {
            return EntityConverter.GetReceipts(await ReceiptFactory.GetInstance().SelectAllAsync());
        }

        public async Task<long> Update(ReceiptService entity)
        {
            return await ReceiptFactory.GetInstance().UpdateAsync(EntityConverter.GetReceipt(entity));
        }
    }
}
