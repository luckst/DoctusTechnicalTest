
namespace Doctus.TechinalTest.Web.Models.Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class HourViewModel : BaseViewModel
    {
        public int HourId { get; set; }
        public int ActivityId { get; set; }
        public int Time { get; set; }
        public DateTime Date { get; set; }
    }
}
