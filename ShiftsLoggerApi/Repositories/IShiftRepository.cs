// IShiftRepository.cs
using System.Collections.Generic;
using ShiftLoggerApi.Models;

public interface IShiftRepository
{
    Task<IEnumerable<Shift>> GetShiftsAsync();
    Task<Shift> GetShiftAsync(int id);
    Task<Shift> CreateShiftAsync(Shift shift);
    Task UpdateShiftAsync(Shift shift);
    Task DeleteShiftAsync(int id);
}
