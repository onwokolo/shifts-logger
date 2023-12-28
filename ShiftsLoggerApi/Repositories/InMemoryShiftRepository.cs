// InMemoryShiftRepository.cs
using System;
using System.Collections.Generic;
using ShiftLoggerApi.Models;

public class InMemoryShiftRepository : IShiftRepository
{
    private readonly List<Shift> _shifts = new()
    {
        new() { Id = 1, UserName = "User1", StartTime = DateTime.Now.AddHours(-4), EndTime = DateTime.Now.AddHours(-2) },
        new() { Id = 2, UserName = "User2", StartTime = DateTime.Now.AddHours(-6), EndTime = DateTime.Now.AddHours(-3) },
        // Add more sample shifts as needed
    };

    public async Task<IEnumerable<Shift>> GetShiftsAsync()
    {
        return await Task.FromResult(_shifts);
    }

    public async Task<Shift> GetShiftAsync(int id)
    {
        var shift = _shifts.Where(s => s.Id == id).SingleOrDefault();
        return await Task.FromResult(shift!);
    }

    public async Task<Shift> CreateShiftAsync(Shift shift)
    {
        // For simplicity, let's generate a unique ID for the new shift
        shift.Id = _shifts.Count + 1;
        _shifts.Add(shift);
        return await Task.FromResult(shift);
    }

    public async Task UpdateShiftAsync(Shift shift)
    {
        var index = _shifts.FindIndex(s => s.Id == shift.Id);
        if (index != -1) // -1 is returned if index is not found
        {
            // Update object where index match
            _shifts[index] = shift;
        }
        await Task.CompletedTask;
    }

    public async Task DeleteShiftAsync(int id)
    {
        var index = _shifts.FindIndex(s => s.Id == id);
        if (index != -1) // -1 is returned if index is not found
        {
            // Remove object where index match
            _shifts.RemoveAt(index);
        }
        await Task.CompletedTask;
    }
}
