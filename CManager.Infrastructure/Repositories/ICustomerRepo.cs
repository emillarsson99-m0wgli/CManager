using CManager.Domain.Models;

namespace CManager.Infrastructure.Repositories;

public interface ICustomerRepo
{
    List<CustomerModel> GetAllCustomers();
    bool SaveCustomer(List<CustomerModel> customer);
}
