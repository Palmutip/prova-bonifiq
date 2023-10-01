using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetAll();
    }
}
