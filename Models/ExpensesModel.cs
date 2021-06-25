using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD_EarningsTracker_AspNetCoreMVC.Models
{
    public class ExpensesModel
    {
        /// <summary>
        /// ID used for sql Data 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The type of expense (e.g: fuel,maintenance)
        /// </summary>
        [Required(ErrorMessage = "Enter the expense type")]
        public string ExpenseType { get; set; }
        /// <summary>
        /// Maintenance dollar amount
        /// </summary>
        [Required(ErrorMessage = "Please enter the dollar amount for the expense")]
        [Range(.01,50000,ErrorMessage = "Please enter the value between .01 - 500000")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// The date and time this expense was done
        /// </summary>
        [Required(ErrorMessage = "Enter the date the expense occurred")]
        public DateTime TimeStamp { get; set; } 
    }
}
