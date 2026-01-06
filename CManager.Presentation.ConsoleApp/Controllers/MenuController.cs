using CManager.Business.Interfaces;
using CManager.Presentation.ConsoleApp.Helpers;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

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
            Console.WriteLine("3. View a specific customer");
            Console.WriteLine("4. Delete customer");
            Console.WriteLine("5. Edit customer");
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
                    GetCustomer();
                    break;
                case "4":
                    DeleteCustomer();
                    break;
                case "5":
                    EditCustomer();
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

        var firstName = InputHelper.ValidateInput("First Name", ValidationType.Required);
        var lastName = InputHelper.ValidateInput("Last Name", ValidationType.Required);
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        var phoneNumber = InputHelper.ValidateInput("Phonenumber", ValidationType.Required);
        var streetName = InputHelper.ValidateInput("StreetName", ValidationType.Required);
        var postalCode = InputHelper.ValidateInput("Postal code", ValidationType.Required);
        var city = InputHelper.ValidateInput("City", ValidationType.Required);


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
                Console.WriteLine();
            }
        }

        OutputDialog("Press any key to continue");
    }


    public void GetCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== Choose customer for more info ===");

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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Choose customer for more info ===");

                for (int i = 0; i < customers.Count(); i++)
                {
                    var getCustomer = customers[i];
                    Console.WriteLine($"[{i + 1} {getCustomer.Id} {getCustomer.FirstName} {getCustomer.LastName}");
                }

                Console.WriteLine("[0] Go back to menu");
                Console.Write("Enter the number of the customer you want to view: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Not a valid number. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }
                if (choice == 0)
                {
                    return;
                }
                if (choice < 1 || choice > customers.Count) //Ai är använt i denna raden då tidigare hade jag endast skrivit "if (choice < customers.Count)" vilket gjorde att användaren avisades om den valde ett tal under antalet kunder. t ex om det fanns 3 kunder och användaren valde 1 så aviserade den att talet måste vara mellan 1 och 3, vilket var felaktigt.
                {
                    Console.WriteLine($"Number must be between 1 and {customers.Count}. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }

                var selectedCustomer = customers[choice - 1]; //Har använt ai här då jag tidigare endast hade skrivit "var selectedCustomer = choice - 1" vilket hade hämtat en int. GetCustomer förväntade sig ett Guid.
                var customer = _customerService.GetCustomer(selectedCustomer.Id);

                if (customer == null)
                {
                    Console.WriteLine("Customer could not be found. Press any key to return to menu.");
                    Console.ReadKey();
                    return;
                }
                Console.Clear();
                Console.WriteLine("=== Customer details ===");
                Console.WriteLine($"Id: {customer.Id}");
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phonenumber: {customer.PhoneNumber}");
                Console.WriteLine($"Adress: {customer.Adress.StreetName} {customer.Adress.PostalCode} {customer.Adress.City}");

                Console.WriteLine("Press any key to go back");
                Console.ReadKey();
                return;
            }
        }
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
                for (int i = 0; i < customers.Count; i++)
                {
                    var customer = customers[i];
                    Console.WriteLine($"[{i + 1} {customer.FirstName} {customer.LastName}");
                }

                Console.WriteLine("[0] Go back to menu");
                Console.Write("Enter number of the customer you want to delete: ");
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


                while (true)
                {
                    Console.WriteLine("Are you sure you want to delete this customer? (y/n): ");
                    var confirmation = Console.ReadLine()!.ToLower();

                    if (confirmation == "y")
                    {
                        var result = _customerService.DeleteCustomer(selectedCustomer.Id);
                        if (result)
                        {
                            OutputDialog($"Customer was removed, press any key to go back");
                            return;
                        }
                        else
                        {
                            OutputDialog($"Something went wrong! Press any key to try again.");
                            return;
                        }
                    }
                    else if (confirmation == "n")
                    {
                        return;
                    }
                    else
                    {
                        OutputDialog("Please enter 'y' for yes or 'n' for no, press any key to try again");
                        continue;
                    }
                }
            }
        }
        OutputDialog("Press any key to continue...");
    }


    private void EditCustomer()
    {
        Console.Clear();
        Console.WriteLine("=== Edit customer ===");

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
            while (true)
            {
                for (int i = 0; i < customers.Count; i++)
                {
                    var customer = customers[i];
                    Console.WriteLine($"[{i + 1} {customer.FirstName} {customer.LastName}");
                }

                Console.WriteLine("[0] Go back to menu");
                Console.Write("Enter number of the customer you want to edit: ");

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
                var selectedCustomer = customers[choice - 1];

                Console.Clear();
                Console.WriteLine("=== Edit Customer ===");
                Console.WriteLine($"Editing: {selectedCustomer.FirstName} {selectedCustomer.LastName}");
                Console.WriteLine();

                var firstName = InputHelper.ValidateInput("First Name", ValidationType.Optional) ?? selectedCustomer.FirstName; // Har använt ai för att kunna behålla tidigare 
                var lastName = InputHelper.ValidateInput("Last Name", ValidationType.Optional) ?? selectedCustomer.LastName;    // kundinformation istället för att returnera ett null värde om jag 
                var email = InputHelper.ValidateInput("Email", ValidationType.Optional) ?? selectedCustomer.Email;              // endast trycker på "Enter"
                var phoneNumber = InputHelper.ValidateInput("Phonenumber", ValidationType.Optional) ?? selectedCustomer.PhoneNumber;
                var streetName = InputHelper.ValidateInput("StreetName", ValidationType.Optional) ?? selectedCustomer.Adress.StreetName;
                var postalCode = InputHelper.ValidateInput("Postal code", ValidationType.Optional) ?? selectedCustomer.Adress.PostalCode;
                var city = InputHelper.ValidateInput("City", ValidationType.Optional) ?? selectedCustomer.Adress.City;

                var result = _customerService.EditCustomer(
                    selectedCustomer.Id,
                    firstName,
                    lastName,
                    email,
                    phoneNumber,
                    streetName,
                    postalCode,
                    city
                    );

                if (result)
                {
                    OutputDialog($"Customer: {selectedCustomer.FirstName} {selectedCustomer.LastName} has been successfully updated.");
                }
                else
                {
                    OutputDialog("Something went wrong! Customer was not updated.");
                }
                return;
            }
        }
    }

    private void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
}
