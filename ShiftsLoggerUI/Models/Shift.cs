// Shift.cs
namespace ShiftsLoggerUI.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }
}