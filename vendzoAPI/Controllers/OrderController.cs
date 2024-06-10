using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vendzoAPI.DTO;
using vendzoAPI.Interfaces;
using vendzoAPI.Models;
using vendzoAPI.Repository;

namespace vendzoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("Orders/all")]
        public IActionResult GetAllOrders()
        {
            var orders = _mapper.Map<List<OrderDTO>>(_orderRepository.GetAllOrders());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(orders);
        }

        [HttpGet("Orders/id={orderId}")]
        public IActionResult GetOrder(string orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();
            
            var order = _mapper
                .Map<OrderDTO>(_orderRepository.GetOrder(orderId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(order);
        }

        [HttpPost("Orders/create")]
        public IActionResult AddOrder([FromBody] OrderDTO orderDto)
        {

            if (!ModelState.IsValid || orderDto == null)
                return BadRequest();

            var order = _mapper.Map<Order>(orderDto);
            order.OrderDate = System.DateTime.Now;
            _userRepository.GetUserById(order.UserId).Orders.Add(order);
            //_userRepository.GetUserById(order.SellerId).Orders.Add(order);

            if ( !_orderRepository.AddOrder(order))
            {
                ModelState.AddModelError("Error", "Failed to add order");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("Orders/delete/hard")]
        public IActionResult DeleteOrder(string id)
        {

            if (string.IsNullOrEmpty(id) || !_orderRepository.OrderExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _orderRepository.GetOrder(id);

            if (!_orderRepository.DeleteOrder(order))
            {
                ModelState.AddModelError("Error", "Failed to delete order");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpDelete("Orders/delete/soft")]
        public IActionResult SoftDeleteOrder(string id)
        {
            if (string.IsNullOrEmpty(id) || !_orderRepository.OrderExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _orderRepository.GetOrder(id);

            if (order.Status == "Deleted")
            {
                ModelState.AddModelError("Error", "Order already deleted");
                return StatusCode(500, ModelState);
            }
            
            order.Status = "Deleted";

            if (!_orderRepository.UpdateOrder(order))
            {
                ModelState.AddModelError("Error", "Failed to delete order");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpGet("OrderEntries/all")]
        public IActionResult GetAllEntries()
        {
            var entries = _mapper
                .Map<ICollection<OrderEntryDTO>>
                (_orderRepository.GetAllEntries());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(entries);
        }

        [HttpGet("OrderEntries/id={entryId}")]
        public IActionResult GetEntry(string entryId)
        {
            if (!_orderRepository.EntryExists(entryId))
                return NotFound();

            var entry = _mapper
                .Map<OrderEntryDTO>(_orderRepository.GetEntry(entryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(entry);
        }

        [HttpPost("OrderEntries/create")]
        public IActionResult AddEntry([FromBody] OrderEntryDTO entryDto)
        {
            if (!ModelState.IsValid || entryDto == null)
                return BadRequest(ModelState);

            var entry = _mapper.Map<OrderEntry>(entryDto);
            _userRepository.GetUserById(entry.BuyerId).BuyerOrderEntries.Add(entry);
            _userRepository.GetUserById(entry.SellerId).SellerOrderEntries.Add(entry);
            entry.CreatedAt = System.DateTime.Now;

            if (!_orderRepository.AddEntry(entry))
            {
                ModelState.AddModelError("Error", "Failed to add entry");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpDelete("OrderEntries/delete/hard")]
        public IActionResult DeleteEntry(string id)
        {
            if (string.IsNullOrEmpty(id) || !_orderRepository.EntryExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entry = _orderRepository.GetEntry(id);

            if (!_orderRepository.DeleteEntry(entry))
            {
                ModelState.AddModelError("Error", "Failed to delete entry");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }



        [HttpGet("OrderEntries/orderId={orderId}")]
        public IActionResult GetEntriesOfOrder(string orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var entries = _mapper
                .Map<ICollection<OrderEntryDTO>>
                (_orderRepository.GetEntriesOfOrder(orderId));

            return Ok(entries);
        }
        /*
        [HttpGet("Orders/buyerId={userId}")]
        public IActionResult GetOrdersOfUser(string userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var orders = _mapper
                .Map<ICollection<OrderDTO>>
                (_orderRepository.GetOrdersOfBuyer(userId));

            return Ok(orders);
        }

        [HttpGet("Orders/sellerId={userId}")]
        public IActionResult GetOrdersOfSeller(string userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var orders = _mapper
                .Map<ICollection<OrderDTO>>
                (_orderRepository.GetOrdersOfSeller(userId));

            return Ok(orders);
        }
        */

        [HttpGet("Orders/userId={userId}")]
        public IActionResult GetOrdersOfUser(string userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var orders = _mapper
                .Map<ICollection<OrderDTO>>
                (_orderRepository.GetOrdersOfUser(userId));

            return Ok(orders);
        }

        [HttpPut("Orders/update")]
        public IActionResult UpdateOrder(string orderId, [FromBody] OrderDTO orderDto)
        {
            if (orderDto == null || !ModelState.IsValid || orderDto.Id != orderId)
                return BadRequest(ModelState);

            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            var order = _mapper.Map<Order>(orderDto);
            if (order == null)
                return NotFound();

            if(!string.IsNullOrEmpty(orderDto.Id))
                order.Id = orderDto.Id;

            if (!string.IsNullOrEmpty(orderDto.UserId))
                order.UserId = orderDto.UserId;

            /*if (!string.IsNullOrEmpty(orderDto.SellerId))
                order.SellerId = orderDto.SellerId;*/

            if (orderDto.OrderDate != null)
                order.OrderDate = orderDto.OrderDate;

            if (orderDto.DeliverDate != null)
                order.DeliverDate = orderDto.DeliverDate;

            if (orderDto.ShipDate != null)
                order.ShipDate = orderDto.ShipDate;

            if (!string.IsNullOrEmpty(orderDto.ShipAddress))
                order.ShipAddress = orderDto.ShipAddress;

            if (!string.IsNullOrEmpty(orderDto.BillAddress))
                order.BillAddress = orderDto.BillAddress;

            if (!string.IsNullOrEmpty(orderDto.TrackingNo))
                order.TrackingNo = orderDto.TrackingNo;

            if (!string.IsNullOrEmpty(orderDto.Status))
                order.Status = orderDto.Status;

            if (orderDto.Total != null && orderDto.Total != 0)
                order.Total = orderDto.Total;


            if (!_orderRepository.UpdateOrder(order))
            {
                ModelState.AddModelError("Error", "Failed to update order");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpPut("OrderEntries/update")]
        public IActionResult UpdateEntry(string entryId, [FromBody] OrderEntryDTO entryDto)
        {
            if (entryDto == null || !ModelState.IsValid || entryDto.Id != entryId)
                return BadRequest(ModelState);

            if (!_orderRepository.EntryExists(entryId))
                return NotFound();

            var entry = _mapper.Map<OrderEntry>(entryDto);
            if (entry == null)
                return NotFound();

            if (!string.IsNullOrEmpty(entryDto.Id))
                entry.Id = entryDto.Id;

            if (!string.IsNullOrEmpty(entryDto.OrderId))
                entry.OrderId = entryDto.OrderId;

            if (!string.IsNullOrEmpty(entryDto.BuyerId))
                entry.BuyerId = entryDto.BuyerId;

            if (!string.IsNullOrEmpty(entryDto.ItemId))
                entry.ItemId = entryDto.ItemId;

            if (!string.IsNullOrEmpty(entryDto.SellerId))
                entry.SellerId = entryDto.SellerId;

            if (!string.IsNullOrEmpty(entryDto.ItemTitle))
                entry.ItemTitle = entryDto.ItemTitle;

            if (!string.IsNullOrEmpty(entryDto.SellerName))
                entry.SellerName = entryDto.SellerName;

            if (!string.IsNullOrEmpty(entryDto.Photo))
                entry.Photo = entryDto.Photo;

            if (entryDto.Price != null || entryDto.Price != 0)
                entry.Price = entryDto.Price;

            if (entryDto.Quantity != null || entryDto.Quantity != 0)
                entry.Quantity = entryDto.Quantity;

            if (!_orderRepository.UpdateEntry(entry))
            {
                ModelState.AddModelError("Error", "Failed to update order entry");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }
    }
}
