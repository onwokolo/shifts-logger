// Shift.cs
using System;

namespace ShiftLoggerApi.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
