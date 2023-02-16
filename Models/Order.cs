using System;
namespace OrderAPI.Models
{
	public class Order
	{
		public Guid Id { get; set; }
		public OrderType Type { get; set; }
		public String? CustomerName { get; set; }
        public DateTime CustomerDate { get; set; }
		public String? CreatedByUsername { get; set; }

	}
}

