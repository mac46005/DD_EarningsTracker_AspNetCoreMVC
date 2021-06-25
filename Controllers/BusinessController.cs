using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DD_EarningsTracker_AspNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DD_EarningsTracker_AspNetCoreMVC.Controllers
{
    public class BusinessController : Controller
    {
        private DDBusiness_DBContext Context;
        public BusinessController(DDBusiness_DBContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            DD_BusinessData_ViewModel model = new DD_BusinessData_ViewModel
            {
                incomeModels = (from i in Context.IncomeList.ToList()
                                orderby i.TimeStamp descending
                                where i.TimeStamp > DateTime.Now.AddDays(CurrentDaysinTheWeek())
                                select i).ToList(),
                expensesModels = (from e in Context.ExpenseList.ToList()
                                  orderby e.TimeStamp descending
                                  where e.TimeStamp > DateTime.Now.AddDays(CurrentDaysinTheWeek())
                                  select e).ToList(),
                mileageModels = (from m in Context.MileageList.ToList()
                                 orderby m.TimeStamp descending
                                 where m.TimeStamp > DateTime.Now.AddDays(CurrentDaysinTheWeek())
                                 select m).ToList()
            };
            SessionsUse();
            return View(model);
        }



        [HttpGet]
        public IActionResult AddUpdateIncome(int id = 0)
        {
            SessionsUse();
            IncomeModel model;
            if (id == 0)
            {
                ViewData["ViewTitle"] = "Add";
                model = new IncomeModel();
                return View(model);
            }
            else
            {
                ViewData["ViewTitle"] = "Update";
                model = Context.IncomeList.Find(id);
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult AddUpdateIncome(IncomeModel model)
        {
            
            if (ModelState.IsValid)
            {
                TempData["message"] = $"You earned ${model.DD_Pay + model.Tips}";
                Context.IncomeList.Add(model);
                Context.SaveChanges();
                SessionsUse();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }



        [HttpGet]
        public IActionResult AddUpdateExpense(int id = 0)
        {
            SessionsUse();
            ExpensesModel model;
            if (id == 0)
            {
                ViewData["ViewTitle"] = "Add";
                model = new ExpensesModel();
                return View(model);
            }
            else
            {
                ViewData["ViewTitle"] = "Update";
                model = Context.ExpenseList.Find(id);
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult AddUpdateExpense(ExpensesModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = $"Spent ${model.Amount} on {model.ExpenseType}";
                Context.ExpenseList.Add(model);
                Context.SaveChanges();
                SessionsUse();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }



        [HttpGet]
        public IActionResult AddUpdateMileage(int id = 0)
        {
            SessionsUse();
            MileageModel model;
            if (id == 0)
            {
                ViewData["ViewTitle"] = "Add";
                model = new MileageModel();
                return View(model);
            }
            else
            {
                ViewData["ViewTitle"] = "Update";
                model = Context.MileageList.Find(id);
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult AddUpdateMileage(MileageModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = $"You have driven {model.Mileage} miles";
                Context.MileageList.Add(model);
                Context.SaveChanges();
                SessionsUse();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }





        [HttpGet]
        [Route("[action]/Type-{type}/ID-{id}")]
        public IActionResult Delete(string type, int id)
        {
            SessionsUse();
            TempData["TempDeleteTitle"] = type;
            DeleteViewModel deleteViewModel = new DeleteViewModel();
            if (type == "Income")
            {
                deleteViewModel.incomeModel = Context.IncomeList.Find(id);
                return View(deleteViewModel);
            }
            else if (type == "Expense")
            {
                deleteViewModel.expensesModel = Context.ExpenseList.Find(id);
                return View(deleteViewModel);
            }
            else if (type == "Mileage")
            {
                deleteViewModel.mileageModel = Context.MileageList.Find(id);
                return View(deleteViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        [Route("[action]/Type-{type}/ID-{id}")]
        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            if (TempData["TempDeleteTitle"].ToString() == "Income")
            {
                TempData["message"] = $"Deleted Income ID:{model.incomeModel.ID} Amount: ${ConvertNullDecimalToString(new decimal?[] { model.incomeModel.DD_Pay,model.incomeModel.Tips})} Time Stamp: {model.incomeModel.TimeStamp.ToString()}"; 
                Context.IncomeList.Remove(model.incomeModel);
                Context.SaveChanges();
                SessionsUse();
                return RedirectToAction("Index");
            }
            else if (TempData["TempDeleteTitle"].ToString() == "Expense")
            {
                TempData["message"] = $"Deleted Expense ID: {model.expensesModel.ID} Type: {model.expensesModel.ExpenseType} Amount: ${ConvertNullDecimalToString(new decimal?[] { model.expensesModel.Amount })} Time Stamp: {model.expensesModel.TimeStamp}";
                Context.ExpenseList.Remove(model.expensesModel);
                Context.SaveChanges();
                SessionsUse();
                return RedirectToAction("Index");

            }
            else if (TempData["TempDeleteTitle"].ToString() == "Mileage")
            {
                TempData["message"] = $"Deleted Mileage ID: {model.mileageModel.ID} Miles: {ConvertNullDecimalToString(new decimal?[] { model.mileageModel.Mileage })}";
                Context.MileageList.Remove(model.mileageModel);
                Context.SaveChanges();
                SessionsUse();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }


        [NonAction]
        public string ConvertNullDecimalToString(decimal?[] nullableDecimalArray)
        {
            decimal amnt = 0;
            foreach (var item in nullableDecimalArray)
            {
                amnt += (decimal)item;
            }
            return amnt.ToString();
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
        [NonAction]
        public static int CurrentDaysinTheWeek()
        {
            int value = (int)DateTime.Now.DayOfWeek;
            return (value + 1) * -1;
        }
    }
}
