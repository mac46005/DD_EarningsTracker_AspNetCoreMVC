using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD_EarningsTracker_AspNetCoreMVC.Models
{
    public class ThisWeekSession
    {
        private const string thisWeekKey = "thisWeek";
        private ISession Session { get; set; }
        public ThisWeekSession(ISession sess) => Session = sess;

        public ThisWeekNavModelView GetThisWeek() => Session.GetObject<ThisWeekNavModelView>(thisWeekKey);
        public void SetThisWeek(ThisWeekNavModelView thisWeekModel) => Session.SetObject<ThisWeekNavModelView>(thisWeekKey, thisWeekModel);
    }
}
