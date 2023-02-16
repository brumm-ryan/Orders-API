using Microsoft.EntityFrameworkCore;
using System;

namespace OrderAPI.Models;

public class OrderContext : DbContext
{
	public OrderContext(DbContextOptions<OrderContext> options) : base(options) {   }
    public DbSet<Order> Orders { get; set; } = null!;
}

