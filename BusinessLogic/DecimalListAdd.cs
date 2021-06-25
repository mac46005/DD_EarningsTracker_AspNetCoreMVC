using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD_EarningsTracker_AspNetCoreMVC.BusinessLogic
{
    public static class DecimalListAdd
    {
        /// <summary>
        /// Adds up a list of a list of decimals.
        /// </summary>
        /// <param name="decimalList">List of list of deimcals</param>
        /// <returns>returns string result of the result</returns>
        public static string AddTotals(List<List<decimal?>> decimalList)
        {
            decimal total = 0;
            foreach (var dl in decimalList)
            {
                foreach (var item in dl)
                {
                    total += (decimal)item;
                }
            }
            return total.ToString();
        }
    }
}
