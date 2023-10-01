using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        TestDbContext _ctx;
        public CustomerRepository(TestDbContext context) : base(context)
        {
            _ctx = context;
        }

        public IQueryable<Customer> GetAll()
        {
            return _ctx.Customers;
        }

        public async Task<IEnumerable<Customer>> GetMany(Expression<Func<Customer, bool>> expression)
        {
            return await _ctx.Customers.Where(expression).ToListAsync();
        }
    }
}
