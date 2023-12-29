// UserInput.cs
using System;
using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI
{
    public static class UserInput
    {
        public static int GetMainMenuResponse()
        {
            while (true)
            {
                Console.WriteLine("MAIN MENU:\n");
                Console.WriteLine("Enter 0 to Exit App");
                Console.WriteLine("Enter 1 to Create/Log a Shift");
                Console.WriteLine("Enter 2 to View all Shifts");
                Console.WriteLine("Enter 3 to View A Shift by ID");
                Console.WriteLine("Enter 4 to View Shifts for certain Username");
                Console.WriteLine("Enter 5 to Update A Shift");
                Console.WriteLine("Enter 6 to Delete A Shift");
                Console.Write("\nWhat would you like to do? ");

                if (int.TryParse(Console.ReadLine()?.Trim(), out int input))
                {
                    if (input >= 0 && input <= 6)
                        return input;
                }

                Console.Clear();
                Console.WriteLine("Invalid option chosen. Please enter a valid number between 0 and 6.\n");

            }
        }
        public static int GetInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine()?.Trim(), out int input))
                {
                    return input;
                }

                Console.Clear();
                Console.WriteLine("Invalid integer value. Please enter a valid number.\n");
            }
        }
        public static string GetString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine("Invalid string value. Please enter a valid string.\n");
            }
        }

        public static DateTime GetDateTime(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                {
                    return result;
                }

                Console.WriteLine("Invalid date format. Please enter a valid date.\n");
            }
        }

        public static (DateTime, DateTime) GetDates(string startPrompt, string endPrompt)
        {
            while (true)
            {
                var startDate = GetDateTime(startPrompt);
                var endDate = GetDateTime(endPrompt);

                if (startDate < endDate)
                {
                    return (startDate, endDate);
                }

                Console.WriteLine("Start date cannot come after end date.\n");
            }
        }

        public static Shift GetShift()
        {
            var userName = GetString("Enter the Username: ");
            var startPrompt = "Enter the start time (yyyy-MM-dd HH:mm:ss): ";
            var endPrompt = "Enter the end time (yyyy-MM-dd HH:mm:ss): ";
            var (startTime, endTime) = GetDates(startPrompt, endPrompt);

            return new Shift
            {
                UserName = userName,
                StartTime = startTime,
                EndTime = endTime,
            };
        }
    }
}