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

                    break;
                case "2":

                    break;
                case "3":

                    break;
                case "0":

                    return;
                default:
                    

            }
        }
    }
}
