
namespace Doctus.TechnicalTest.Domain.Entities
{
    using System;

    public class Activity
    {
        public int ActivityId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
