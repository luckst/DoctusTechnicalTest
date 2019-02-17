
namespace Doctus.TechnicalTest.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Hour
    {
        public int HourId { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
        public int Time { get; set; }
        public DateTime Date { get; set; }
    }
}
