using CManager.Business.Interfaces;
using CManager.Business.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repositories;
using NSubstitute;

namespace CManager.Tests.Application;

public class CustomerService_Test
{
    [Fact]
    public void CreateCustomer_ShouldReturnTrue_IfCustomerIsCreatedSuccessfully()  //Stor del av detta kodstycket är ai genererat, 
    {
        //Arrange 
        var mockCustomerRepo = Substitute.For<ICustomerRepo>(); //Skapar en "fake" implementation av min ICustomerRepo. Den låtsas vara ICustomerRepo och returnernar dem värdena jag vill att den ska returnera.
        var customers = new List<CustomerModel>(); //Skapar en riktig lista.

        mockCustomerRepo.GetAllCustomers().Returns(customers);
        mockCustomerRepo.SaveCustomer(Arg.Any<List<CustomerModel>>()).Returns(true);

        var service = new CustomerService(mockCustomerRepo); //Skapar en riktig CustomerService som sedan mitt mockRepo injeceras in i.

        //Act
        var result = service.CreateCustomer(  //Bygger en CustomerModel med värdena jag har gett den och hämtar my mockRepo som den sedan lägger in kunden i. Och kommer sedan returnera ett "true"värde som jag har angett.
            firstName: "Test",
            lastName: "Testsson",
            email: "test@domain.com",
            phoneNumber: "1234567890",
            StreetName: "Testgatan",
            postalCode: "123 45",
            city: "Testropolis"
            );

        //Assert
        Assert.True(result);

        mockCustomerRepo.Received(1).SaveCustomer(Arg.Any<List<CustomerModel>>()); //Ser till att min SaveCustomer hämtas endast 1 gång och ser till att den inte sparar mer än 1 gång.

        mockCustomerRepo.Received(1).SaveCustomer(Arg.Is<List<CustomerModel>>(List => List.Count == 1 && // Kollar så att det finns exakt en kund i den sparade listan och ser till att  alla fälten matchar med vad jag har angett i min "Act" del.
        List[0].FirstName == "Test" &&
        List[0].LastName == "Testsson" &&
        List[0].Email == "test@domain.com" &&
        List[0].PhoneNumber == "1234567890" &&
        List[0].Adress.StreetName == "Testgatan" &&
        List[0].Adress.PostalCode == "123 45" &&
        List[0].Adress.City == "Testropolis" &&
        List[0].Id != Guid.Empty

        ));
    }



}