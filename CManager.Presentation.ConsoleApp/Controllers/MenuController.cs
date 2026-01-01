using CManager.Business.Services;
using System.Diagnostics.CodeAnalysis;

namespace CManager.Presentation.ConsoleApp.Controllers;

public class MenuController
{
    private readonly ICustomerService _customerService;

    public MenuController([NotNull] ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Customer Management System ===");
            Console.WriteLine("1. Add new customer");
            Console.WriteLine("2. View all customers");
            Console.WriteLine("3. Delete Customer");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddNewCustomer();
                    break;
                case "2":
                    ViewAllCustomers();
                    break;
                case "3":
                    DeleteCustomer();
                    break;
                case "0":
                    return;
                default:
                    OutputDialog("Invalid option... Press any key to continue.");
            }
        }
    }

    private void AddNewCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== Add New Customer ===");

        var firstName = ;
        var lastName = ;
        var email = ;
        var phoneNumber = ;
        var streetName = ;
        var postalCode = ;
        var city = ;


        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetName, postalCode, city);

        if (result)
        {
            Console.WriteLine("The customer has been added successfully.");
            Console.WriteLine($"Name: {firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong... The customer could not be added.");
        }
        OutputDialog("Press any key to continue.");
    }

    private void OutputDialog(string message)
    {
        Console.WriteLine("");
        Console.ReadKey();
    }
}
