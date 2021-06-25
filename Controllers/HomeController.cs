using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DD_EarningsTracker_AspNetCoreMVC.Models;

namespace DD_EarningsTracker_AspNetCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private DDBusiness_DBContext Context;
        public HomeController(DDBusiness_DBContext ctx)
        {
            Context = ctx;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {

            DD_BusinessData_ViewModel model = new DD_BusinessData_ViewModel
            {
                incomeModels = Context.IncomeList.ToList(),
                expensesModels = Context.ExpenseList.ToList(),
                mileageModels = Context.MileageList.ToList()
            };
            SessionsUse();



            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        void SetDataView(string count,string income,string expense,string mileage)
        {
            ViewData["count"] = count;
            ViewData["income"] = income;
            ViewData["expense"] = expense;
            ViewData["mileage"] = mileage;
        }
    }
}
