using CManager.Domain.Factories;
using CManager.Domain.Models;

namespace CManager.Business.Services;

public class CustomerService : ICustomerService
{


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
    }
}
