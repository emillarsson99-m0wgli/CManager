using CManager.Business.Interfaces;
using CManager.Domain.Factories;
using CManager.Domain.Models;

namespace CManager.Business.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepo _CustomerRepo;

    public CustomerService(ICustomerRepo customerRepo)
    {
        _CustomerRepo = customerRepo;
    }

    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string StreetName, string postalCode, string city)
    {
        CustomerModel customerModel = new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Adress = new AdressModel
            {
                StreetName = StreetName,
                PostalCode = postalCode,
                City = city
            }
        };
        try
        {
            var customers = _CustomerRepo.GetAllCustomers();
            customers.Add(customerModel);
            var result = _CustomerRepo.SaveCustomer(customers);
            return result;
        }
         catch (Exception)
        {
            return false;
        }
    }

    public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
    {
        hasError = false;

        try
        {
            var customers = _CustomerRepo.GetAllCustomers();
            return customers;
        }
        catch (Exception)
        {
            hasError = true;
            return [];
        }
    }

    public CustomerModel? GetCustomer(Guid id)
    {
        var customers = _CustomerRepo.GetAllCustomers();
        return customers?.FirstOrDefault(c => c.Id == id); //Har tagit hjälp av ai för denna metoden
        // Frågeteckenet efter "customers" ser till att vi inte får undantag om "customers" är null.
        // Utan detta så hade repon krashat om det inte fanns några kunder.
    }

    public bool DeleteCustomer(Guid id)
    {
        try
        {
            var customers = _CustomerRepo.GetAllCustomers();
            var customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return false;

            customers.Remove(customer);
            var result = _CustomerRepo.SaveCustomer(customers);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting customer: {ex.Message}");
            return false;
        }
    }

    public bool EditCustomer(Guid id, string firstName, string lastName, string email, string phoneNumber, string streetName, string postalCode, string city)
    {
        try
        {
            var customers = _CustomerRepo.GetAllCustomers();
            var customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return false;

            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Email = email;
            customer.PhoneNumber = phoneNumber;
            customer.Adress.StreetName = streetName;
            customer.Adress.PostalCode = postalCode;
            customer.Adress.City = city;

            var result = _CustomerRepo.SaveCustomer(customers);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error editing customer: {ex.Message}");
            return false;
        }
    }
}
