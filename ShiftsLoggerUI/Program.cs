// Program.cs
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ShiftsLoggerUI;
using ShiftsLoggerUI.Models;

var httpClient = new HttpClient();
var shiftLoggerApiClient = new ShiftLoggerApiClient(httpClient);

var exit = false;

while (!exit)
{
    switch (UserInput.GetMainMenuResponse())
    {
        case 0:
            exit = true;
            break;
        case 1:
            await HandleCreate();
            break;
        case 2:
            await HandleViewAll();
            break;
        case 3:
            await HandleViewShiftByID();
            break;
        case 4:
            await HandleViewShiftsByUsername();
            break;
        case 5:
            await HandleUpdateShift();
            break;
        case 6:
            await HandleDeleteShift();
            break;
        default:
            Console.WriteLine("Invalid option chosen...\n");
            break;
    }
}

Console.WriteLine("Exiting...");

async Task HandleCreate()
{
    var shift = UserInput.GetShift();

    try
    {
        await shiftLoggerApiClient.LogShiftAsync(shift);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    await Task.CompletedTask;
}

async Task HandleViewAll()
{
    var allShifts = await shiftLoggerApiClient.GetAllShiftsAsync();
    DisplayShifts(allShifts);

    await Task.CompletedTask;
}

async Task HandleViewShiftByID()
{
    var shiftId = UserInput.GetInt("Enter the shift ID to fetch: ");
    var shiftById = await shiftLoggerApiClient.GetShiftByIdAsync(shiftId);

    if (shiftById != null)
    {
        DisplayShifts(new[] { shiftById });
    }
}

async Task HandleViewShiftsByUsername()
{
    var specificUsername = UserInput.GetString("Enter the username to fetch shifts: ");
    var shiftsByUsername = await shiftLoggerApiClient.GetShiftsByUsernameAsync(specificUsername);
    DisplayShifts(shiftsByUsername);
}

async Task HandleUpdateShift()
{
    var shiftIdToUpdate = UserInput.GetInt("Enter the shift ID to update: ");
    var updatedShift = UserInput.GetShift();
    await shiftLoggerApiClient.UpdateShiftByIdAsync(shiftIdToUpdate, updatedShift);
}

async Task HandleDeleteShift()
{
    var shiftIdToDelete = UserInput.GetInt("Enter the shift ID to delete: ");
    await shiftLoggerApiClient.DeleteShiftByIdAsync(shiftIdToDelete);
}

static void DisplayShifts(IEnumerable<Shift> shifts)
{
    Console.WriteLine("All Shifts:\n");

    if (!shifts.Any())
    {
        Console.WriteLine("No shifts found!");
    }

    foreach (var shift in shifts)
    {
        Console.WriteLine($"Id: {shift.Id}, User: {shift.UserName}, Start Time: {shift.StartTime}, End Time: {shift.EndTime}, Duration: {shift.Duration}");
    }
    Console.WriteLine();
}