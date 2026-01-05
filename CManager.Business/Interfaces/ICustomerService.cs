using CManager.Domain.Models;

namespace CManager.Business.Interfaces;

public interface ICustomerService
{
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string StreetName, string postalCode, string city);
    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);
    bool DeleteCustomer(Guid id);

    bool EditCustomer(
        Guid id,
        string? firstName = null,
        string? lastName = null,
        string? email = null,
        string? phoneNumber = null,
        string? streetName = null,
        string? postalCode = null,
        string? city = null);
}
