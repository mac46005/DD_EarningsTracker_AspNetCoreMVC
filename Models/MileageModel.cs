using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD_EarningsTracker_AspNetCoreMVC.Models
{
    /// <summary>
    /// This is the Mileage class model
    /// </summary>
    public class MileageModel
    {
        /// <summary>
        /// id used for sql data
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The amount of mileage done for the day
        /// </summary>
        [Required(ErrorMessage = "Please enter the Mileage amount completed today")]
        [Range(0.1,1000,ErrorMessage = "Please enter a value 0.1 - 1000")]
        public decimal? Mileage { get; set; }
        /// <summary>
        /// The date this is event is recorded
        /// </summary>
        [Required(ErrorMessage = "PLease enter the date for this event")]
        public DateTime TimeStamp { get; set; }
    }
}
