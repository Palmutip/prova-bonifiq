using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IQueryable<Customer> GetAll();
    }
}
