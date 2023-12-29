// ShiftLoggerApiClient.cs
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI
{
    public class ShiftLoggerApiClient
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://localhost:5031";

        public ShiftLoggerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(ApiBaseUrl);
        }

        public async Task LogShiftAsync(Shift shift)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/shifts", shift);

            response.EnsureSuccessStatusCode();

            Console.WriteLine("Shift logged successfully!\n");
        }

        public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
        {
            var response = await _httpClient.GetAsync("/api/shifts");

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<IEnumerable<Shift>>())!;
        }

        public async Task<IEnumerable<Shift>> GetShiftsByUsernameAsync(string username)
        {
            var response = await _httpClient.GetAsync($"/api/shifts?username={username}");

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<IEnumerable<Shift>>())!;
        }

        public async Task<Shift> GetShiftByIdAsync(int shiftId)
        {
            var response = await _httpClient.GetAsync($"/api/shifts/{shiftId}");

            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<Shift>())!;
            }

            // Handle the case where the shift with the specified ID is not found
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Shift with ID {shiftId} not found.\n");
                return null!;
            }

            response.EnsureSuccessStatusCode(); // Handles other unexpected errors
            return null!;
        }

        public async Task<bool> UpdateShiftByIdAsync(int shiftId, Shift updatedShift)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/shifts/{shiftId}", updatedShift);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Shift with ID {shiftId} updated successfully.\n");
                return true;
            }

            // Handle the case where the shift with the specified ID is not found
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Shift with ID {shiftId} not found.\n");
                return false;
            }

            // Handle other unexpected errors
            response.EnsureSuccessStatusCode();
            return false;
        }

        public async Task<bool> DeleteShiftByIdAsync(int shiftId)
        {
            var response = await _httpClient.DeleteAsync($"/api/shifts/{shiftId}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Shift with ID {shiftId} deleted successfully.\n");
                return true;
            }

            // Handle the case where the shift with the specified ID is not found
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Shift with ID {shiftId} not found.\n");
                return false;
            }

            // Handle other unexpected errors
            response.EnsureSuccessStatusCode();
            return false;
        }

    }
}