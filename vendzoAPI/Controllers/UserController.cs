using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vendzoAPI.DTO;
using vendzoAPI.Interfaces;
using vendzoAPI.Models;

namespace vendzoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IAddressRepository addressRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("all")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers() { 
            
            var users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
              
        }
        [HttpGet("find/id={id}")]
        public IActionResult GetUser(string id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            var user = _mapper.Map<UserDTO>(_userRepository.GetUserById(id));
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }
        [HttpGet("find/username={username}")]
        public IActionResult GetUserByUsername(string username)
        {
            var user = _mapper.Map<UserDTO>(_userRepository.GetUserByUsername(username));
            if(user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }
        [HttpGet("find/email={email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _mapper.Map<UserDTO>(_userRepository.GetUserByEmail(email));
            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest(ModelState);

            /*
            var user = _userRepository.GetUsers()
                .Where(c => c.Username.Trim().ToUpper() == userDTO.Username.TrimEnd().ToUpper())
                .FirstOrDefault();
            */

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check username 
            if (_userRepository.GetUserByUsername(userDTO.Username.TrimEnd()) != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            //check email
            if (_userRepository.GetUserByEmail(userDTO.Email.TrimEnd()) != null)
            {
                ModelState.AddModelError("", "Email already exists");
                return StatusCode(422, ModelState);
            }

            var userMap = _mapper.Map<User>(userDTO);

            userMap.CreatedAt = DateTime.Now;

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong with creating the user :(");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (userId != userDTO.Id)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _userRepository.GetUserById(userId);
            if (userMap == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(userDTO.Username) && 
                _userRepository.UserExistsByUsername(userDTO.Username) && 
                _userRepository.GetUserByUsername(userDTO.Username).Id != userMap.Id )
            {
                ModelState.AddModelError("", "Username already exists");
                return StatusCode(422, ModelState);
            }

            if (!string.IsNullOrWhiteSpace(userDTO.Email) &&
                _userRepository.GetUserByEmail(userDTO.Email) != null &&
                _userRepository.GetUserByEmail(userDTO.Email).Id != userMap.Id )
            {
                ModelState.AddModelError("", "Email already exists");
                return StatusCode(422, ModelState);
            }

            

            var userIdentity = await _userManager.FindByIdAsync(userMap.LoginId);
            if (userIdentity == null)
            {
                return NotFound("User not found");
            }

            // Update only the provided fields
            if (!string.IsNullOrWhiteSpace(userDTO.Username) && userDTO.Username != userMap.Username)
            {
                userMap.Username = userDTO.Username;

                // Update username
                var setUserNameResult = await _userManager.SetUserNameAsync(userIdentity, userDTO.Username);
                if (!setUserNameResult.Succeeded)
                {
                    return BadRequest(setUserNameResult.Errors);
                }
            }
            

            if (!string.IsNullOrWhiteSpace(userDTO.Email) && userDTO.Email != userMap.Email)
            {
                userMap.Email = userDTO.Email;

                var setEmailResult = await _userManager.SetEmailAsync(userIdentity, userDTO.Email);
                if (!setEmailResult.Succeeded)
                {
                    return BadRequest(setEmailResult.Errors);
                }
            }
                

            if (!string.IsNullOrWhiteSpace(userDTO.CurrentAddress))
                userMap.CurrentAddress = userDTO.CurrentAddress;

            if (!string.IsNullOrWhiteSpace(userDTO.ContactNo))
                userMap.ContactNo = userDTO.ContactNo;

                userMap.IsClient = userDTO.IsClient;


            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong with the updating the user :(");
                return StatusCode(500, ModelState);

            }
            return Ok();
        }

        [HttpDelete("delete/hard")]
        public IActionResult DeleteUser(string userId)
        {
            if(userId == null || !_userRepository.UserExists(userId))
            { return NotFound(); }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userToDelete = _userRepository.GetUserById(userId);

            //TODO: Add relation validation (eg: user-orders, user-adresses)

            //userToDelete.IsDeleted = true; //bu niye burda ???

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with the deletion of the user :(");
                return StatusCode(500, ModelState);

            }

            return Ok("Success");

        }
        [HttpDelete("delete/soft")]
        public IActionResult SoftDeleteUser(string userId)
        {
            if (userId == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return NotFound();

            if (user.IsDeleted)
            {
                ModelState.AddModelError("", "User is already soft deleted.");
                return StatusCode(500, ModelState);

            }

            user.IsDeleted = true;

            //TODO: Add relation validation (eg: user-orders, user-adresses)


            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong with soft deleting the user :(");
                return StatusCode(500, ModelState);

            }
            return Ok("Success");
        }
        [HttpPut("setDefaultAddress")]
        public IActionResult SetDefaultAddress(string userId,string addressId)
        {
            if (userId == null || addressId == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.GetUserById(userId);
            var address = _addressRepository.Get(addressId);
            if (user == null || address == null)
                return NotFound();

            if (user.IsDeleted || address.IsDeleted)
            {
                ModelState.AddModelError("", "User or address is soft deleted.");
                return StatusCode(500, ModelState);

            }

            if (!_userRepository.SetDefaultAddressOfUser(user,address,true))
            {
                ModelState.AddModelError("", "Something went wrong with seting default address of the user :(");
                return StatusCode(500, ModelState);

            }
            return Ok("Success");
        }
    }
}
