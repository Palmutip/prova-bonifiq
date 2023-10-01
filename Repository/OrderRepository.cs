using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;

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
    }
}
