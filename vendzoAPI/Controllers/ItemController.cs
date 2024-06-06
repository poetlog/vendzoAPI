using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vendzoAPI.DTO;
using vendzoAPI.Interfaces;
using vendzoAPI.Models;

namespace vendzoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ItemController(IMapper mapper, IUserRepository userRepository, IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetItems()
        {
            var items = _mapper.Map<List<ItemDTO>>(_itemRepository.GetItems());

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(items);
        }

        [HttpGet("all/page={page}")]
        public IActionResult GetItems(int page) {
            var items = _mapper.Map<List<ItemDTO>>(_itemRepository.GetItemsPagination(page));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(items);
        }

        [HttpGet("search/{searchTerm}/page={page}")]
        public IActionResult SearchItems(string searchTerm, int page)
        {
            var items = _mapper.Map<List<ItemDTO>>(_itemRepository.SearchItems(searchTerm, page));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(items);
        }

        [HttpGet("filter/{category}/page={page}")]
        public IActionResult FilterItems(string category, int page)
        {
            var items = _mapper.Map<List<ItemDTO>>(_itemRepository.FilterItems(category, page));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(items);
        }

        [HttpGet("sort/{sortTerm}/page={page}")]
        public IActionResult SortItems(string sortTerm, int page)
        {
            var items = _mapper.Map<List<ItemDTO>>(_itemRepository.SortItems(sortTerm, page));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(items);
        }

        [HttpGet("count")]
        public IActionResult GetItemsCount()
        {
            var count = _itemRepository.GetItemsCount();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(count);
        }

        [HttpGet("featured")]
        public IActionResult GetFeaturedItems()
        {
            var items = _mapper.Map<List<ItemDTO>>(_itemRepository.GetFeaturedItems());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(items);
        }

        [HttpGet("find/id={id}")]
        public IActionResult GetItem(string id) 
        {
            if (!_itemRepository.ItemExists(id))
                return NotFound();

            var item = _mapper.Map<ItemDTO>(_itemRepository.GetItem(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(item);
        }

        [HttpGet("find/category={category}")]
        public IActionResult GetItemsOfCategory(string category)
        {
            var items = _mapper.Map<List<ItemDTO>>(_itemRepository.GetItemsOfCategory(category));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(items);
        }

        [HttpGet("find/user={userId}")]
        public IActionResult GetItemsOfUser(string userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!_userRepository.GetUserById(userId).IsClient)
                return BadRequest(ModelState);

            var items = _mapper.Map<List<ItemDTO>>(_itemRepository.GetItemsOfUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(items);
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult CreateItem([FromBody] ItemDTO itemDTO)
        {
            if (itemDTO == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var itemMap = _mapper.Map<Item>(itemDTO);
            var seller = _userRepository.GetUserById(itemDTO.SellerId);
            itemMap.Seller = seller;
            itemMap.CreatedAt = DateTime.Now;
            seller.Items.Add(itemMap);

            if(!_itemRepository.Add(itemMap))
            {
                ModelState.AddModelError("", "Something went wrong with adding the item :(");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPut("update")]
        [Authorize]
        public IActionResult UpdateItem(string itemId, [FromBody] ItemDTO itemDTO)
        {
            if (itemDTO == null || !ModelState.IsValid || itemId != itemDTO.Id)
                return BadRequest("Error ln105");

            if (!_itemRepository.ItemExists(itemId))
                return NotFound();

            var itemMap = _itemRepository.GetItem(itemId);
            if (itemMap == null)
                return NotFound();

            if (!string.IsNullOrEmpty(itemDTO.Id))
            {
                itemMap.Id = itemDTO.Id;
            }

            if (!string.IsNullOrEmpty(itemDTO.SellerId))
            {
                itemMap.SellerId = itemDTO.SellerId;
            }

            if (!string.IsNullOrEmpty(itemDTO.Description))
            {
                itemMap.Description = itemDTO.Description;
            }

            if (!string.IsNullOrEmpty(itemDTO.Title))
            {
                itemMap.Title = itemDTO.Title;
            }

            if (!string.IsNullOrEmpty(itemDTO.Category))
            {
                itemMap.Category = itemDTO.Category;
            }

            if (itemDTO.Price.HasValue)
            {
                itemMap.Price = itemDTO.Price.Value;
            }

            if (!string.IsNullOrEmpty(itemDTO.Photo))
            {
                itemMap.Photo = itemDTO.Photo;
            }

            if (itemDTO.Stock.HasValue)
            {
                itemMap.Stock = itemDTO.Stock.Value;
            }

            if(!_itemRepository.Update(itemMap))
            {
                ModelState.AddModelError("", "Something went wrong with updating the item :(");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("delete/hard")]
        [Authorize]

        public IActionResult DeleteItem(string id)
        {
            if (string.IsNullOrEmpty(id) || !_itemRepository.ItemExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemToDelete = _itemRepository.GetItem(id);

            //TODO: Add relation validation (eg: items-users, items-orders etc.)

            if (itemToDelete == null || _itemRepository.Delete(itemToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with the deletion of the item :(");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("delete/soft")]
        [Authorize]

        public IActionResult SoftDeleteItem(string id)
        {
            if (string.IsNullOrEmpty(id) || !_itemRepository.ItemExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemToDelete = _itemRepository.GetItem(id);

            if(itemToDelete == null)
                return NotFound();
            
            if(itemToDelete.IsDeleted)
            {
                ModelState.AddModelError("", "Item is already soft deleted.");
                return StatusCode(500, ModelState);

            }
            itemToDelete.IsDeleted = true;
            

            if (!_itemRepository.Update(itemToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with soft deleting the item :(");
                return StatusCode(500, ModelState);

            }
            return Ok();
        }

    }
}
