using CManager.Business.Services;
using System.ComponentModel.Design;
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
                    break;
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


    private void ViewAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("=== All Customers ===");

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong! Please try again!");
        }
        if (!customers.Any())
        {
            Console.WriteLine("No customers found!");
        }
        else
        {
            foreach(var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phonenumber: {customer.PhoneNumber}");
                Console.WriteLine($"Adress: {customer.Adress.StreetName} {customer.Adress.PostalCode} {customer.Adress.City}");
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine();
            }
        }

        OutputDialog("Press any key to continue");
    }


    private void DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== Delete customer ===");

        var customers = _customerService.GetAllCustomers(out bool hasError).ToList();

        if (hasError)
        {
            Console.WriteLine("Something went wrong! Please try again!");
        }
        if (!customers.Any())
        {
            Console.WriteLine("No customers found!");
        }
        else
        {
            while(true)
            {
                for (int i = 0; i < customers.Count(); i++)
                {
                    var customer = customers[i];
                    Console.WriteLine($"[{i + 1} {customer.FirstName} {customer.LastName}");
                }

                Console.WriteLine("[0] Go back to menu");
                Console.Write("Enter number of the customer you want to delete");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    OutputDialog("Not a valid number! Press any key to try again.");
                    continue;
                }
                if (choice == 0)
                {
                    return;
                }
                if (choice > customers.Count)
                {
                    Console.WriteLine($"Number must be between 1 and {customers.Count}. Press any key to try again");
                    Console.ReadKey();
                    continue;
                }

                var index = choice - 1;
                var selectedCustomer = customers[index];

                Console.WriteLine("You have selected: ");
                Console.WriteLine($"Name: {selectedCustomer.FirstName} {selectedCustomer.LastName}");

                Console.WriteLine("Are you sure you want to delete this customer=");
                var confirmation = Console.ReadLine()!.ToLower();

                if (confirmation == "y")
                {
                    var result = _customerService.DeleteCustomer(selectedCustomer.Id);
                    if (result)
                    {
                        OutputDialog("Customer was removed, press any key to go back");
                        break;
                    }
                    else
                    {
                        OutputDialog($"Something went wrong! Press any key to try again.");
                        return;
                    }
                }
                else if (confirmation == "n")
                {
                    break;
                }
                else
                {
                    OutputDialog("Please enter 'y' for yes or 'n' for no, press any key to try again");
                }
            }
        }
        OutputDialog("Press any key to continue...");
    }

    private void OutputDialog(string message)
    {
        Console.WriteLine("");
        Console.ReadKey();
    }
}
