using Microsoft.EntityFrameworkCore;
using Moq;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using System.Linq.Expressions;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task CanPurchase_InvalidCustomerId_ThrowsException()
        {
            var service = new CustomerService(new Mock<ICustomerRepository>().Object, new Mock<IOrderRepository>().Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => service.CanPurchase(0, 100));
            Assert.Equal("Cliente não encontrado.", exception.Message);
        }

        [Fact]
        public async Task CanPurchase_InvalidPurchaseValue_ThrowsException()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) =>
                {
                    var customers = new List<Customer>
                    {
            new Customer { Id = 1, Name = "Cliente de Teste" },
                    };
                    return customers.SingleOrDefault(predicate.Compile());
                });

            var service = new CustomerService( customerRepositoryMock.Object, new Mock<IOrderRepository>().Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => service.CanPurchase(1, -100));
            Assert.Equal("Não é possivel realizar pagamentos com valor R$0,00.", exception.Message);
        }

        [Fact]
        public async Task CanPurchase_CustomerAlreadyPurchasedThisMonth_ReturnsFalse()
        {
            var ordersData = new List<Order>
    {
        new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now },
    };

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) =>
                {
                    var customers = new List<Customer>
                    {
                new Customer { Id = 1, Name = "Cliente de Teste" },
                    };
                    return customers.SingleOrDefault(predicate.Compile());
                });

            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetMany(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Expression<Func<Order, bool>> predicate) =>
                {
                    return ordersData.Where(predicate.Compile());
                });

            var service = new CustomerService(customerRepositoryMock.Object, orderRepositoryMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => service.CanPurchase(1, 100));
            Assert.Equal("O cliente ja fez uma compra esse mês.", exception.Message);
        }

        [Fact]
        public async Task CanPurchase_FirstTimeCustomerExceedsLimit_ReturnsFalse()
        {
            var customersData = new List<Customer>
                {
                    new Customer { Id = 2, Name = "Cliente de Teste" },
                };
            var ordersData = new List<Order>
                {
                    new Order { Id = 1, CustomerId = 2, OrderDate = DateTime.Now },
                };

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) =>
                {
                    var customers = new List<Customer>
                    {
            new Customer { Id = 1, Name = "Cliente de Teste" },
                    };
                    return customers.SingleOrDefault(predicate.Compile());
                });
            customerRepositoryMock.Setup(repo => repo.GetAll()).Returns(customersData.AsQueryable());

            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetMany(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Expression<Func<Order, bool>> predicate) =>
                {
                    return ordersData.Where(predicate.Compile());
                });

            var service = new CustomerService(customerRepositoryMock.Object, orderRepositoryMock.Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => service.CanPurchase(1, 101));
            Assert.Equal("A primeira compra do cliente tem um limite de 100 reais.", exception.Message);
        }
    }
}
