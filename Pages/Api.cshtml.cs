using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomerAPI.Models;
using CustomerAPI.DB;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Pages
{

	public class ApiModel : PageModel
	{
		private readonly CustomerDbContext _db;
		const int NUM_OF_CUSTOMERS = 50000;
		public ApiModel(CustomerDbContext DB)
		{
			_db = DB;
		}

		public JsonResult? OnGet(int? id, int? limit)
		{
			try
			{
				//?ID QUERY PRESENT
				if (id != null)
				{
					//400
					if (id == 0 || id > 50000)
					{
						Response.StatusCode = 400;
						ErrorMessage err = new ErrorMessage();
						err.Msg = "id of " + id + " does not exist";
						return new JsonResult(err);
					}

					Response.StatusCode = 200;
					Customer? customer = _db.customers.Find(id);
					return new JsonResult(customer);
				}
				//?LIMIT QUERY PRESENT
				else if (limit != null)
				{
					//400
					if (limit <= 1 || limit > 1000)
					{
						Response.StatusCode = 400;
						ErrorMessage err = new ErrorMessage();
						err.Msg = "Limit must be greater than 1 and less than or equal to 1,000";
						return new JsonResult(err);
					}

					Response.StatusCode = 200;
					Random r = new Random();
					int randomStartIndex = r.Next(0, (int)(NUM_OF_CUSTOMERS - limit)); //makes sure range of customers selected is not out of bounds
					List<Customer> customers = _db.customers.AsNoTracking().ToList().GetRange(randomStartIndex, (int)limit);
					return new JsonResult(customers);
				}
				//default return
				//random customer
				else
				{
					Response.StatusCode = 200;
					Random rand = new Random();
					int randomID = rand.Next(0, NUM_OF_CUSTOMERS);
					Customer? customer = _db.customers.Find(randomID);

					return new JsonResult(customer);
				}
			}
			//500
			catch (Exception err)
			{
				Response.StatusCode = 500;
				return new JsonResult(err);
			}
		}
	}
	class ErrorMessage
	{
		public string? Msg { get; set; }
	}
}
