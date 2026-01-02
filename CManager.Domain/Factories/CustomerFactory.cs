using CManager.Domain.Models;

namespace CManager.Domain.Factories;

public static class CustomerFactory
{
    public static CustomerModel Create()
    {
        return new CustomerModel();
    }

    public static CustomerModel Create(CustomerCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new ArgumentException($"{nameof(request.FirstName)} is required.");

        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new ArgumentException($"{nameof(request.LastName)} is required.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException($"{nameof(request.Email)} is required.");


        var customer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Adress = request.Adress
        };

        return customer;
    }
}
