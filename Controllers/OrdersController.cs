using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("/Orders/")]
    public class OrdersController : ControllerBase
    {

        private readonly OrderContext _orderContext;

        public OrdersController(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            return await _orderContext.Orders.Select(x => OrderToDTO(x)).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
        {
            var order = await _orderContext.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return OrderToDTO(order);
        }


        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
            {
                return BadRequest();
            }


            var order = await _orderContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderContext.Entry(order).State = EntityState.Modified;
            order.CreatedByUsername = orderDTO.CreatedByUsername;
            order.CustomerName = orderDTO.CustomerName;
            order.CustomerDate = orderDTO.CustomerDate;
            OrderType changedOrderType;
            if(Enum.TryParse<OrderType>(orderDTO.Type, out changedOrderType))
            {
                order.Type = changedOrderType;
            }

            try
            {
                await _orderContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder(OrderDTO orderDTO)
        {
            // use standard as the default value for OrderType
            OrderType orderType = OrderType.Standard;
            if (orderDTO.Type != null)
            {
                Enum.TryParse<OrderType>(orderDTO.Type, out orderType);
            }

            var order = new Order
            {
                Id = orderDTO.Id,
                CustomerName = orderDTO.CustomerName,
                CustomerDate = orderDTO.CustomerDate,
                CreatedByUsername = orderDTO.CreatedByUsername,
                Type = orderType
            };

            _orderContext.Orders.Add(order);
            await _orderContext.SaveChangesAsync();

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, OrderToDTO(order));

        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _orderContext.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderContext.Orders.Remove(order);
            await _orderContext.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Orders/FilterByType/{orderType}
        [HttpGet("FilterByType/{orderType}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetFilteredOrders(OrderType orderType)
        {
            var orders = await _orderContext.Orders.ToListAsync();
            if (orders == null)
            {
                return NotFound();
            }

            return orders.Where(o => o.Type == orderType).Select(o => OrderToDTO(o)).ToList();
        }

        private bool OrderExists(Guid id)
        {
            return _orderContext.Orders.Any(e => e.Id == id);
        }

        private static OrderDTO OrderToDTO(Order order) =>
            new OrderDTO
            {
               Id = order.Id,
               Type = Enum.GetName(typeof(OrderType), order.Type),
               CustomerName = order.CustomerName,
               CustomerDate = order.CustomerDate,
               CreatedByUsername = order.CreatedByUsername,

            };

    }
}

