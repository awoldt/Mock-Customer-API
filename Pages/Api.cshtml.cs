using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomerAPI.Models;
using CustomerAPI.DB;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Pages
{

	public class ApiModel : PageModel
	{
        const int NUM_OF_CUSTOMERS = 50000;
        private readonly CustomerDbContext _db;
		[FromQuery(Name = "id")]
		public int? idQuery { get; set; }
		[FromQuery(Name = "limit")]
		public int? limitQuery { get; set; }
        public ApiModel(CustomerDbContext DB)
        {
            _db = DB;
        }

        //returns json data instead of HTML page
        public JsonResult OnGet()
		{
			try
			{
				//?ID QUERY PRESENT
				if (idQuery != null)
				{
					//400
					if (idQuery == 0 || idQuery > 50000)
					{
						Response.StatusCode = 400;
						ErrorMessage err = new ErrorMessage();
						err.Msg = "id of " + idQuery + " does not exist";
						return new JsonResult(err);
					}

					Response.StatusCode = 200;
					Customer customer = _db.customers.Find(idQuery);
					return new JsonResult(customer);
				}
				//?LIMIT QUERY PRESENT
				else if (limitQuery != null)
				{
					//400
					if (limitQuery <= 1 || limitQuery > 1000)
					{
						Response.StatusCode = 400;
						ErrorMessage err = new ErrorMessage();
						err.Msg = "Limit must be greater than 1 and less than or equal to 1,000";
						return new JsonResult(err);
					}

					Response.StatusCode = 200;
					Random r = new Random();
					int randomStartIndex = r.Next(0, (int)(NUM_OF_CUSTOMERS - limitQuery)); //makes sure range of customers selected is not out of bounds
					List<Customer> customers = _db.customers.AsNoTracking().ToList().GetRange(randomStartIndex, (int)limitQuery);
					return new JsonResult(customers);
				}
				//default return
				//random customer
				else
				{
					Response.StatusCode = 200;
					Random rand = new Random();
					int randomID = rand.Next(0, NUM_OF_CUSTOMERS);
					Customer customer = _db.customers.Find(randomID);

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
		public string Msg { get; set; }
	}
}
