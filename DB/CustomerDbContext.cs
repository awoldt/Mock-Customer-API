using Microsoft.EntityFrameworkCore;
using CustomerAPI.Models;

namespace CustomerAPI.DB
{
	public class CustomerDbContext: DbContext
	{
		public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) {}

		public DbSet<Customer> customers { get; set; }
	}
}
