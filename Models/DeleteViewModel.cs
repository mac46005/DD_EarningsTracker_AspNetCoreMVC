using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD_EarningsTracker_AspNetCoreMVC.Models
{
    /// <summary>
    /// Class model used for the Delete View page
    /// </summary>
    public class DeleteViewModel
    {
        public IncomeModel incomeModel { get; set; }
        public ExpensesModel expensesModel { get; set; }
        public MileageModel mileageModel { get; set; }
    }
}
