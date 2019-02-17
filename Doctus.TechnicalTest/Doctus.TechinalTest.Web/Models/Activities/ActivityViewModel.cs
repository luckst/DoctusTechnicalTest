
namespace Doctus.TechinalTest.Web.Models.Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ActivityViewModel : BaseViewModel
    {
        public int ActivityId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
    }
}
