using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DD_EarningsTracker_AspNetCoreMVC.Models;

namespace DD_EarningsTracker_AspNetCoreMVC.Controllers
{
    public class ListController : Controller
    {
        private DDBusiness_DBContext Context;
        public ListController(DDBusiness_DBContext ctx)
        {
            Context = ctx;
        }

        public IActionResult Index()
        {
            SessionsUse();
            return View();
        }
        public IActionResult IncomeDataList()
        {
            SessionsUse();
            List<IncomeModel> listOfIncomeModels = (from i in Context.IncomeList.ToList()
                                                    orderby i.TimeStamp descending
                                                    select i).ToList();
            return View(listOfIncomeModels);
        }
        public IActionResult ExpenseDataList()
        {
            SessionsUse();
            List<ExpensesModel> listOfExpenseModels = (from e in Context.ExpenseList.ToList()
                                                       orderby e.TimeStamp descending
                                                       select e).ToList();
            return View(listOfExpenseModels);
        }
        public IActionResult MileageDataList()
        {
            SessionsUse();
            List<MileageModel> listOfMileageModels = (from m in Context.MileageList.ToList()
                                                      orderby m.TimeStamp descending
                                                      select m).ToList();
            return View(listOfMileageModels);
        }





        [NonAction]
        public void SessionsUse()
        {
            ThisWeekSession thisWeekSession = new ThisWeekSession(HttpContext.Session);
            ThisWeekNavModelView week = new ThisWeekNavModelView(
                Context.IncomeList.Where(i => i.TimeStamp > DateTime.Now.AddDays(ThisWeekNavModelView.CurrentDaysinTheWeek())).ToList(),
                Context.ExpenseList.Where(e => e.TimeStamp > DateTime.Now.AddDays(ThisWeekNavModelView.CurrentDaysinTheWeek())).ToList(),
                Context.MileageList.Where(m => m.TimeStamp > DateTime.Now.AddDays(ThisWeekNavModelView.CurrentDaysinTheWeek())).ToList()
                );
            thisWeekSession.SetThisWeek(week);

            SetDataView(
                week.GetCount().ToString(),
                week.GetIncome().ToString("C"),
                week.GetExpense().ToString("C"),
                week.GetMiles().ToString()
                );

        }
        [NonAction]
        void SetDataView(string count, string income, string expense, string mileage)
        {
            ViewData["count"] = count;
            ViewData["income"] = income;
            ViewData["expense"] = expense;
            ViewData["mileage"] = mileage;
        }

    }
}
