using System.Text.RegularExpressions;

namespace CManager.Presentation.ConsoleApp.Helpers;

public enum ValidationType
{
    Required,
    Email
}

public static class InputHelper
{
    public static string ValidateInput(string fieldName, ValidationType validationType)
    {
        while (true)
        {
            Console.Write($"{fieldName}: ");
            var input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"{fieldName} is required. Press any key to try again.");
                Console.ReadKey();
                continue;
            }
            var (isValid, errorMessage) = ValidateByType(input, validationType);
            if (isValid)
                return input;

            Console.WriteLine($"{errorMessage}. Press any key to continue.");
            Console.ReadKey();
        }
    }

    private static (bool isValid, string errorMessage) ValidateByType(string input, ValidationType type)
    {
        switch (type)
        {
            case ValidationType.Required:
                return (true, "");

            case ValidationType.Email:
                if (isValidEmail(input))
                {
                    return (true, "");
                }
                else
                {
                    return (false, "Invalid email format. Please use name@example.com");
                }

            default:
                return (true, "");
        }
    }

    private static bool isValidEmail(string input)
    {
        var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"; //Regex är ai genererad
        return Regex.IsMatch(input, pattern);
    }
}

