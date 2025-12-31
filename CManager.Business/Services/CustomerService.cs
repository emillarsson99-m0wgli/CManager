using CManager.Domain.Models;
using CManager.Infrastructure.Repositories;

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
}
