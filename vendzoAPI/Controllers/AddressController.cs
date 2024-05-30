using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vendzoAPI.DTO;
using vendzoAPI.Interfaces;

namespace vendzoAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressController(IUserRepository userRepo, IAddressRepository addressRepo, IMapper mapper)
        {
            _userRepository = userRepo;
            _addressRepository = addressRepo;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAddresses() 
        {
            var addresses = _mapper.Map<List<AddressDTO>>(_addressRepository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(addresses);
        }
        [HttpGet("find/id={id}")]
        public IActionResult GetAddress(string id)
        {
            if (!_addressRepository.AddressExists(id))
                return NotFound();

            var address = _mapper.Map<AddressDTO>(_addressRepository.Get(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(address);
        }

        [HttpGet("find/userId={userId}")]
        public IActionResult GetAddressByUser(string userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var address = _mapper.Map<List<AddressDTO>>(_addressRepository.GetByUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(address);
        }

        [HttpPost("create")]
        public IActionResult CreateAddress([FromBody] AddressDTO addressDTO) 
        {
            if (addressDTO == null || addressDTO.UserId == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.GetUserById(addressDTO.UserId);
            if (user == null)
                return NotFound();

            var addressMap = _mapper.Map<Address>(addressDTO);
            addressMap.CreatedAt = DateTime.Now;
            addressMap.User = user;
            _userRepository.AddAddressToUser(user, addressMap,false);

            if (!_addressRepository.CreateAddress(addressMap))
            {
                ModelState.AddModelError("", "Something went wrong with creating the address :(");
                return StatusCode(500, ModelState);
            }
            return Ok("Success");
        }

        [HttpPut("update")]
        public IActionResult UpdateAddress(string addressId, [FromBody] AddressDTO addressDTO)
        {
            if (addressDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (addressId != addressDTO.Id)
                return BadRequest(ModelState);

            if (!_addressRepository.AddressExists(addressId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var addressMap = _addressRepository.Get(addressId);
            if (addressMap == null)
            {
                return NotFound();
            }

            // Update only the provided fields
            if (!string.IsNullOrWhiteSpace(addressDTO.UserId))
                addressMap.UserId = addressDTO.UserId;

            if (!string.IsNullOrWhiteSpace(addressDTO.Address1))
                addressMap.Address1 = addressDTO.Address1;

            if (!string.IsNullOrWhiteSpace(addressDTO.ContactNo))
                addressMap.ContactNo = addressDTO.ContactNo;

            if (!string.IsNullOrWhiteSpace(addressDTO.Title))
                addressMap.Title = addressDTO.Title;

            if (!_addressRepository.UpdateAddress(addressMap))
            {
                ModelState.AddModelError("", "Something went wrong with the updating the address :(");
                return StatusCode(500, ModelState);

            }
            return Ok("Success");
        }

        [HttpDelete("delete/hard")]
        public IActionResult DeleteAddress(string addressId)
        {
            if (addressId == null || !_addressRepository.AddressExists(addressId))
            { return NotFound(); }

            var addressToDelete = _addressRepository.Get(addressId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //TODO: Add relation validation (eg: user-orders, user-adresses)

            addressToDelete.IsDeleted = true;

            if (!_addressRepository.UpdateAddress(addressToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with the deletion of the address :(");
            }

            return Ok("Success");

        }
        [HttpDelete("delete/soft")]
        public IActionResult SoftDeleteAddress(string addressId)
        {
            if (addressId == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var address = _addressRepository.Get(addressId);
            if (address == null)
                return NotFound();

            if (address.IsDeleted)
            {
                ModelState.AddModelError("", "Address is already soft deleted.");
                return StatusCode(500, ModelState);

            }

            address.IsDeleted = true;

            if (!_addressRepository.UpdateAddress(address))
            {
                ModelState.AddModelError("", "Something went wrong with soft deleting the address :(");
                return StatusCode(500, ModelState);

            }
            return Ok("Success");
        }

    }
}
