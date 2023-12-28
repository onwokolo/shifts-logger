// ShiftsController.cs
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShiftLoggerApi.Dtos;
using ShiftLoggerApi.Models;
using ShiftLoggerApi.Utils;

// [Route("api/[controller]")]
[Route("api/shifts")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly IShiftRepository _shiftRepository;
    private readonly ILogger<ShiftsController> _logger;

    public ShiftsController(IShiftRepository shiftRepository, ILogger<ShiftsController> logger)
    {
        _shiftRepository = shiftRepository;
        _logger = logger;
    }

    // GET /shifts
    [HttpGet]
    public async Task<IEnumerable<ShiftDto>> GetShiftsAsync(string username = "")
    {
        var shifts = (await _shiftRepository.GetShiftsAsync()).Select(item => item.AsDto());

        if (!string.IsNullOrWhiteSpace(username))
        {
            shifts = shifts.Where(shift => shift.UserName.Contains(username, StringComparison.OrdinalIgnoreCase));
        }

        _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {shifts.Count()} items");

        return shifts;
    }

    // GET /shifts/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ShiftDto>> GetShiftAsync(int id)
    {
        var shift = await _shiftRepository.GetShiftAsync(id);
        if (shift is null)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss tt")}: Shift with id '{id}' was not found!");

            // Return 400 status with appropriate or empty body
            return new ContentResult
            {
                StatusCode = 400,
                Content = $"{{\"message\": \"Shift with id '{id}' was not found!\"}}",
                ContentType = "application/json"
            };
        }

        _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss tt")}: Shift with id '{id}' was retrieved!");

        return Ok(shift.AsDto());
    }

    // POST /shifts
    [HttpPost]
    public async Task<ActionResult<ShiftDto>> CreateShiftAsync(CreateShiftDto shiftDto)
    {
        Shift shift = new()
        {
            UserName = shiftDto.UserName,
            StartTime = shiftDto.StartTime,
            EndTime = shiftDto.EndTime,
        };

        shift = await _shiftRepository.CreateShiftAsync(shift);

        return shift.AsDto();
    }

    // PUT /shifts/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateShiftAsync(int id, UpdateShiftDto shiftDto)
    {
        var existingShift = await _shiftRepository.GetShiftAsync(id);


        if (existingShift is null)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss tt")}: Shift with id '{id}' was not found!");

            // Return 400 status with appropriate or empty body
            return new ContentResult
            {
                StatusCode = 400,
                Content = $"{{\"message\": \"Shift with id '{id}' was not found!\"}}",
                ContentType = "application/json"
            };
        }

        existingShift.UserName = shiftDto.UserName;
        existingShift.StartTime = shiftDto.StartTime;
        existingShift.EndTime = shiftDto.EndTime;

        await _shiftRepository.UpdateShiftAsync(existingShift);

        return NoContent();
    }

    // DELETE /shifts/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteShiftAsync(int id)
    {
        var existingShift = await _shiftRepository.GetShiftAsync(id);

        if (existingShift is null)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss tt")}: Shift with id '{id}' was not found!");

            // Return 400 status with appropriate or empty body
            return new ContentResult
            {
                StatusCode = 400,
                Content = $"{{\"message\": \"Shift with id '{id}' was not found!\"}}",
                ContentType = "application/json"
            };
        }

        await _shiftRepository.DeleteShiftAsync(id);

        return NoContent();
    }
}
