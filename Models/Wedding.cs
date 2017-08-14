using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Wedding : BaseEntity
    {
// -------------------------------------------------
        public int WeddingId { get; set; }
// -------------------------------------------------
        public string BrideName { get; set; }
// -------------------------------------------------
        public string GroomName { get; set; }
// -------------------------------------------------
        public DateTime Date { get; set; }
// -------------------------------------------------
        public string Address { get; set; }
// -------------------------------------------------
        public int Creator { get; set; }
// -------------------------------------------------
        public DateTime CreatedAt { get; set; }
// -------------------------------------------------
        public DateTime UpdatedAt { get; set; }
// -------------------------------------------------
        public List<Attendance> Guests { get; set; }
        public Wedding()
        {
            Guests = new List<Attendance>();
        }
// -------------------------------------------------
    }
}