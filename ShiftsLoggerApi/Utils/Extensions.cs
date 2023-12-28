using ShiftLoggerApi.Dtos;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Utils
{
    public static class Extensions
    {
        public static ShiftDto AsDto(this Shift shift)
        {
            return new ShiftDto(shift.Id, shift.UserName, shift.StartTime, shift.EndTime, (
                int)(shift.EndTime - shift.StartTime).TotalMinutes);
        }
    }
}