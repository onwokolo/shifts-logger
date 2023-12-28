using System.ComponentModel.DataAnnotations;

namespace ShiftLoggerApi.Dtos
{
    public record ShiftDto(int Id, [Required] string UserName, DateTime StartTime, DateTime EndTime, int Duration);
    public record CreateShiftDto([Required] string UserName, DateTime StartTime, DateTime EndTime);
    public record UpdateShiftDto([Required] string UserName, DateTime StartTime, DateTime EndTime);
}