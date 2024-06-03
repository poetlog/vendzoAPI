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

    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IUserRepository userRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetBaskets() 
        {
            var baskets = _mapper.Map<List<BasketDTO>>(_basketRepository.GetAllBaskets());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(baskets);
        }

        [HttpGet("find/id={id}")]
        public IActionResult GetBasketById(string id)
        {
            if(!_basketRepository.BasketExists(id))
                return NotFound();

            var basket = _mapper.Map<BasketDTO>(_basketRepository.GetBasket(id));
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(basket);
        }

        [HttpGet("find/user={userId}&item={itemId}")]
        public IActionResult GetBasketById(string userId, string itemId)
        {
            var basket = _mapper.Map<BasketDTO>(_basketRepository.GetBasket(userId,itemId));

            if (!ModelState.IsValid)
                return BadRequest();

            if (basket == null)
                return NotFound();

            return Ok(basket);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetBasketOfUser(string userId)
        {
            var baskets = _mapper.Map<List<BasketDTO>>(_basketRepository.GetBasketOfUser(userId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(baskets);
        }

        [HttpPost("create")]
        public IActionResult CreateBasket([FromBody] BasketDTO basketDTO)
        {
            if (basketDTO == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var basketMap = _mapper.Map<Basket>(basketDTO);
            basketMap.CreatedAt = DateTime.Now;
            _userRepository.GetUserById(basketDTO.UserId).Baskets.Add(basketMap);

            if (!_basketRepository.Add(basketMap))
            {
                ModelState.AddModelError("", "Something went wrong with adding the basket :(");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");

        }

        [HttpPut("update")]
        public IActionResult UpdateBasket(string basketId, [FromBody] BasketDTO basketDTO)
        {
            if (!ModelState.IsValid || basketDTO == null || basketDTO.Id != basketId)
                return BadRequest(ModelState);

            if (!_basketRepository.BasketExists(basketId))
                return NotFound();

            var basketMap = _basketRepository.GetBasket(basketId);
            if (basketMap == null)
                return NotFound();

            if (!string.IsNullOrEmpty(basketDTO.UserId))
            {
                basketMap.UserId = basketDTO.UserId;
            }

            if (!string.IsNullOrEmpty(basketDTO.ItemId))
            {
                basketMap.ItemId = basketDTO.ItemId;
            }

            if (basketDTO.Quantity.HasValue)
            {
                basketMap.Quantity = basketDTO.Quantity.Value;
            }

            if (!_basketRepository.Update(basketMap))
            {
                ModelState.AddModelError("", "Something went wrong with updating the basket :(");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        [HttpDelete("delete/hard")]
        public IActionResult DeleteBasket(string basketId) 
        {
            if (string.IsNullOrEmpty(basketId) || !_basketRepository.BasketExists(basketId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var basketToDelete = _basketRepository.GetBasket(basketId);
            //TODO: add relations
            if (basketToDelete == null || _basketRepository.Delete(basketToDelete))            
            {
                ModelState.AddModelError("", "Something went wrong with the deletion of the basket :(");
                return StatusCode(500, ModelState);
            }
            return Ok("Success");
        }

        [HttpDelete("delete/soft")]
        public IActionResult SoftDeleteBasket(string basketId)
        {
            if (string.IsNullOrEmpty(basketId) || !_basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var basketToDelete = _basketRepository.GetBasket(basketId);

            if (basketToDelete == null)
                return NotFound();

            if (basketToDelete.IsDeleted)
            {
                ModelState.AddModelError("", "Basket is already soft deleted.");
                return StatusCode(500, ModelState);

            }
            basketToDelete.IsDeleted = true;


            if (!_basketRepository.Update(basketToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with soft deleting the basket :(");
                return StatusCode(500, ModelState);

            }
            return Ok("Success");
        }
    }
}
