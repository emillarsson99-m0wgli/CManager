using CManager.Domain.Models;

namespace CManager.Business.Services;

public interface ICustomerService
{
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string StreetName, string postalCode, string city);
    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);
}
