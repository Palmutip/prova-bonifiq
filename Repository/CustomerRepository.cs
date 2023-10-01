using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;

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
    }
}
