using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetAll();
        Task<IEnumerable<Order>> GetMany(Expression<Func<Order, bool>> expression);
    }
}
