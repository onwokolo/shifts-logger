// ShiftRepository.cs
using System.Collections.Generic;
using ShiftLoggerApi.Models;
using ShiftLoggerApi.Data;
using Microsoft.EntityFrameworkCore;

public class SqlServerShiftRepository : IShiftRepository
{
    private readonly AppDbContext _context;

    public SqlServerShiftRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shift>> GetShiftsAsync()
    {
        return await Task.FromResult(await _context.Shifts.ToListAsync());
    }

    public async Task<Shift> GetShiftAsync(int id)
    {
        return await Task.FromResult((await _context.Shifts.FindAsync(id))!);
    }

    public async Task<Shift> CreateShiftAsync(Shift shift)
    {
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();
        return await Task.FromResult(shift);
    }

    public async Task UpdateShiftAsync(Shift shift)
    {
        _context.Entry(shift).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        await Task.CompletedTask;
    }

    public async Task DeleteShiftAsync(int id)
    {
        var shift = _context.Shifts.Find(id);

        if (shift != null)
        {
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
        }

        await Task.CompletedTask;
    }
}
