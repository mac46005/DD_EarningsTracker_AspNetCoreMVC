using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD_EarningsTracker_AspNetCoreMVC.Models
{
    /// <summary>
    /// This is the Income Class model.
    /// </summary>
    public class IncomeModel
    {
        /// <summary>
        /// id used for sql data
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Door Dash base pay
        /// </summary>
        [Required(ErrorMessage = "Please enter the amount Door Dash pays")]
        [Range(0,500,ErrorMessage = "Must enter a value 0 - 500")]
        public decimal? DD_Pay { get; set; }
        /// <summary>
        /// The tips recieved for the order
        /// </summary>
        [Required(ErrorMessage = "Please enter the tip recieved for the order")]
        [Range(0,500,ErrorMessage = "Please enter a value between 0 - 500")]
        public decimal? Tips { get; set; }
        /// <summary>
        /// The time and date the order was done
        /// </summary>
        [Required(ErrorMessage = "Please enter the date this order was completed on")]
        public DateTime TimeStamp { get; set; }
    }
}
