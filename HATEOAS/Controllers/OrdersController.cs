using Microsoft.AspNetCore.Mvc;

namespace HATEOAS.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders = new List<Order>
    {
        new Order { Id = 1, Status = "Pending" },
        new Order { Id = 2, Status = "Confirmed" }
    };

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            var response = new OrderResponseModel
            {
                Id = order.Id,
                Status = order.Status,
            };

            response.Links.Add(new LinkModel
            {
                Href = Url.Action(nameof(GetOrder), new { id = order.Id }),
                Rel = "self",
                Method = "GET"
            });

            if (order.Status == "Pending")
            {
                response.Links.Add(new LinkModel
                {
                    Href = Url.Action("ConfirmOrder", new { id = order.Id }),
                    Rel = "confirm",
                    Method = "POST"
                });

                response.Links.Add(new LinkModel
                {
                    Href = Url.Action("CancelOrder", new { id = order.Id }),
                    Rel = "cancel",
                    Method = "POST"
                });
            }
            else if (order.Status == "Confirmed")
            {
                response.Links.Add(new LinkModel
                {
                    Href = Url.Action("PayOrder", new { id = order.Id }),
                    Rel = "pay",
                    Method = "POST"
                });
            }

            return Ok(response);
        }

        [HttpPost("{id}/confirm")]
        public IActionResult ConfirmOrder(int id)
        {
            return Ok($"Order {id} confirmed.");
        }

        [HttpPost("{id}/cancel")]
        public IActionResult CancelOrder(int id)
        {
            return Ok($"Order {id} canceled.");
        }

        [HttpPost("{id}/pay")]
        public IActionResult PayOrder(int id)
        {
            return Ok($"Order {id} paid.");
        }
    }

}
