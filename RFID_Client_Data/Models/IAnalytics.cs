using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    public interface IAnalytics<T>
    {
        /// <summary>
        /// <see cref="T"/> collection for period
        /// <para>Period start and period end are included</para>
        /// </summary>
        /// <param name="startDate">Start of the period</param>
        /// <param name="endDate">End of the period</param>
        /// <returns></returns>
        Task<List<T>> GetPeriodAsync(DateTime startDate, DateTime endDate);
        
    }
}
