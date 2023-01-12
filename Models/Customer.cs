using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAPI.Models
{
	public class Customer
	{
		[Key]
		[Column("id")]
		public int ID { get; set; }
		[Column("first_name")]
		public string First_Name { get; set; }
		[Column("last_name")]
		public string Last_Name { get; set; }
		[Column("email")]
		public string Email { get; set; }
		[Column("phone_number")]
		public string Phone_Number { get; set; }
		[Column("address")]
		public string Address { get; set; }
		[Column("card_number")]
		public string Card_Number { get; set; }
		[Column("order_total")]
		public string Order_Total { get; set; }
	}
}
