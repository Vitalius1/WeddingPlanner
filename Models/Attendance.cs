using System;
namespace WeddingPlanner.Models
{
    public class Attendance : BaseEntity
    {
// --------------------------------------------
        public int AttendanceId { get; set; }
// --------------------------------------------
        public int UserId { get; set; }
        public User User { get; set; }
// --------------------------------------------
        public int WeddingId { get; set; }
        public Wedding Wedding { get; set; }
// -------------------------------------------------
        public DateTime CreatedAt { get; set; }
// -------------------------------------------------
        public DateTime UpdatedAt { get; set; }
// -------------------------------------------------
    }
}