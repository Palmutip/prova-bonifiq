using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IQueryable<Customer> GetAll();
        Task<IEnumerable<Customer>> GetMany(Expression<Func<Customer, bool>> expression);
    }
}
