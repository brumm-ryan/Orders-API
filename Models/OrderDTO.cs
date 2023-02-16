using System;
namespace OrderAPI.Models
{
	public class OrderDTO
	{

        public Guid Id { get; set; }
        public String? Type { get; set; }
        public String? CustomerName { get; set; }
        public DateTime CustomerDate { get; set; }
        public String? CreatedByUsername { get; set; }

	}
}

