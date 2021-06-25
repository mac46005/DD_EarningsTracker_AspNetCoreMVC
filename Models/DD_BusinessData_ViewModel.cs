using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD_EarningsTracker_AspNetCoreMVC.Models
{
    public class DD_BusinessData_ViewModel
    {
        public List<IncomeModel> incomeModels { get; set; }
        public List<ExpensesModel> expensesModels { get; set; }
        public List<MileageModel> mileageModels { get; set; }
    }
}
