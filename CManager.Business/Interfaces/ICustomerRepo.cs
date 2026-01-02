using CManager.Domain.Models;

namespace CManager.Business.Interfaces;

public interface ICustomerRepo
{
    List<CustomerModel> GetAllCustomers();
    bool SaveCustomer(List<CustomerModel> customer);
}
