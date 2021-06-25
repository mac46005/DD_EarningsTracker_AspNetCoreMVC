using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DD_EarningsTracker_AspNetCoreMVC.BusinessLogic;

namespace DD_EarningsTracker_AspNetCoreMVC.Models
{
    /// <summary>
    /// Model used to show the current weeks earnigns expenses and mileage
    /// </summary>
    public class ThisWeekNavModelView
    {
        private static decimal income; 
        private static decimal expense;
        private static decimal miles;
        private static int orderCount;

        private List<decimal?> ddPayList;
        private List<decimal?> tipsList;
        private List<decimal?> expensesList;
        private List<decimal?> mileageList;

        public ThisWeekNavModelView(List<IncomeModel> il,List<ExpensesModel> el,List<MileageModel> ml)
        {
            ddPayList = il.Select(t => t.DD_Pay).ToList();
            tipsList = il.Select(t => t.Tips).ToList();
            expensesList = el.Select(t => t.Amount).ToList();
            mileageList = ml.Select(t => t.Mileage).ToList();

            income = ReturnResult(ddPayList) + ReturnResult(tipsList);
            expense = ReturnResult(expensesList);
            miles = ReturnResult(mileageList);
            orderCount = ddPayList.Count();
        }
        /// <summary>
        /// Current day of the week
        /// </summary>
        /// <returns>Returns the amount of days currently in the week and returns it as a negative value get those days.</returns>
        public static int CurrentDaysinTheWeek()
        {
            int value = (int)DateTime.Now.DayOfWeek;
            return (value + 1) * -1;
        }
        /// <summary>
        /// Adds up a list of double values and returns the value
        /// </summary>
        /// <param name="decimalList">accepts a list of double values</param>
        /// <returns>returns the total double amount of the list provided </returns>
        decimal ReturnResult(List<decimal?> decimalList)
        {
            decimal value = 0;
            foreach (var item in decimalList)
            {
                value += (decimal)item;
            }
            return value;
        }

        public decimal GetIncome() => income;
        public decimal GetExpense() => expense;
        public decimal GetMiles() => miles;
        public int GetCount() => orderCount;
    }
}
