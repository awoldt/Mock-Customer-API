using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomerAPI.DB;
using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Pages
{
	public class DownloadModel : PageModel
	{
		const int NUM_OF_CUSTOMERS = 50000;
		private readonly CustomerDbContext _db;
		[FromQuery(Name = "limit")]
		public int limitQuery { get; set; }

		public DownloadModel(CustomerDbContext DB)
		{
			_db = DB;
		}

		public JsonResult OnGet()
		{
			try
			{
				//fetch the database of customers 
				//send back a random indexs from the list
				List<Customer> customers = _db.customers.AsNoTracking().ToList();

				if (limitQuery > 1000 || limitQuery < 2)
				{
					return new JsonResult(null);
				}

				List<Customer> RandomCustomers = new List<Customer>();
				List<int> RandomIndexes = new List<int>();

				while (RandomCustomers.Count != limitQuery)
				{
					Random rand = new Random();
					int RandomIndex = rand.Next(0, NUM_OF_CUSTOMERS);
					if (!RandomIndexes.Contains(RandomIndex))
					{
						RandomIndexes.Add(RandomIndex);
						RandomCustomers.Add(customers[RandomIndex]);
					}

				}
				return new JsonResult(RandomCustomers);
			}
			catch (Exception ex)
			{
				return new JsonResult(ex);
			}
		}
	}
}
