using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD_EarningsTracker_AspNetCoreMVC.Models
{
    /// <summary>
    /// Inherits from DbContext. Use this to communicate with the DDBusiness Database.
    /// </summary>
    public class DDBusiness_DBContext : DbContext
    {
        /// <summary>
        /// Income model sql access
        /// </summary>
        public DbSet<IncomeModel> IncomeList { get; set; }
        /// <summary>
        /// Expense model sql access
        /// </summary>
        public DbSet<ExpensesModel> ExpenseList { get; set; }
        /// <summary>
        /// Mileage model sql access
        /// </summary>
        public DbSet<MileageModel> MileageList { get; set; }
        public DDBusiness_DBContext(DbContextOptions<DDBusiness_DBContext> options):base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
