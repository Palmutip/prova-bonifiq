using AutoMapper;
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
            var _autoMapperProfile = new Automap();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);
            var service = new CustomerService(_mapper, new Mock<ICustomerRepository>().Object, new Mock<IOrderRepository>().Object);

            var exception = await Assert.ThrowsAsync<Exception>(() => service.CanPurchase(0, 100));
            Assert.Equal("Cliente não encontrado.", exception.Message);
        }

        [Fact]
        public async Task CanPurchase_InvalidPurchaseValue_ThrowsException()
        {
            var _autoMapperProfile = new Automap();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns((Expression<Func<Customer, bool>> predicate) =>
                {
                    if (predicate.Compile()(new Customer { Id = 1 }))
                    {
                        return new Customer { Id = 1, Name = "Cliente de Teste" };
                    }
                    return null;
                });

            var service = new CustomerService(_mapper, customerRepositoryMock.Object, new Mock<IOrderRepository>().Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.CanPurchase(1, -100));
            Assert.Equal("Não é possivel realizar pagamentos com valor R$0,00.", exception.Message);
        }

        [Fact]
        public async Task CanPurchase_CustomerAlreadyPurchasedThisMonth_ReturnsFalse()
        {
            var ordersData = new List<Order>
                {
                    new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now },
                    // Adicione outros objetos Order conforme necessário
                };
            var _autoMapperProfile = new Automap();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns((Expression<Func<Customer, bool>> predicate) =>
                {
                    if (predicate.Compile()(new Customer { Id = 1 }))
                    {
                        return new Customer { Id = 1, Name = "Cliente de Teste" };
                    }
                    return null;
                });

            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetAll()).Returns(ordersData.AsQueryable());

            var service = new CustomerService(_mapper, customerRepositoryMock.Object, orderRepositoryMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.CanPurchase(1, 100));
            Assert.Equal("O cliente ja fez uma compra esse mês.", exception.Message);

        }

        [Fact]
        public async Task CanPurchase_FirstTimeCustomerExceedsLimit_ReturnsFalse()
        {
            var customersData = new List<Customer>
                {
                    new Customer { Id = 2, Name = "Cliente de Teste" },
                    // Adicione outros objetos Customer conforme necessário
                };
            var ordersData = new List<Order>
                {
                    new Order { Id = 1, CustomerId = 2, OrderDate = DateTime.Now },
                    // Adicione outros objetos Order conforme necessário
                };
            var _autoMapperProfile = new Automap();
            var _configuration = new MapperConfiguration(x => x.AddProfile(_autoMapperProfile));
            IMapper _mapper = new Mapper(_configuration);

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<Customer, bool>>>()))
                .Returns((Expression<Func<Customer, bool>> predicate) =>
                {
                    if (predicate.Compile()(new Customer { Id = 1 }))
                    {
                        return new Customer { Id = 2, Name = "Cliente de Teste" };
                    }
                    return null;
                });
            customerRepositoryMock.Setup(repo => repo.GetAll()).Returns(customersData.AsQueryable());

            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetAll()).Returns(ordersData.AsQueryable());

            var service = new CustomerService(_mapper, customerRepositoryMock.Object, orderRepositoryMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.CanPurchase(1, 101));
            Assert.Equal("A primeira compra do cliente tem um limite de 100 reais.", exception.Message);
        }
    }
}
