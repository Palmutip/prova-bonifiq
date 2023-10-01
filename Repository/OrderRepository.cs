using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        TestDbContext _ctx;
        public OrderRepository(TestDbContext context) : base(context)
        {
            _ctx = context;
        }

        public IQueryable<Order> GetAll()
        {
            return _ctx.Orders;
        }

        public async Task<IEnumerable<Order>> GetMany(Expression<Func<Order, bool>> expression)
        {
            return await _ctx.Orders.Where(expression).ToListAsync();
        }

    }
}
