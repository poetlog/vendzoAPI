using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vendzoAPI.DTO;
using vendzoAPI.Interfaces;

namespace vendzoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
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

            //check password

            if (userDTO.Pass == null || userDTO.Pass.Length < 7)
            {
                ModelState.AddModelError("", "Weak password");
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
        public IActionResult UpdateUser(string userId, [FromBody] UserDTO userDTO)
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

            // Update only the provided fields
            if (!string.IsNullOrWhiteSpace(userDTO.Username))
                userMap.Username = userDTO.Username;

            if (!string.IsNullOrWhiteSpace(userDTO.Pass))
                userMap.Pass = userDTO.Pass;

            if (!string.IsNullOrWhiteSpace(userDTO.Email))
                userMap.Email = userDTO.Email;

            if (!string.IsNullOrWhiteSpace(userDTO.CurrentAddress))
                userMap.CurrentAddress = userDTO.CurrentAddress;

            if (!string.IsNullOrWhiteSpace(userDTO.ContactNo))
                userMap.ContactNo = userDTO.ContactNo;

            if (!string.IsNullOrWhiteSpace(userDTO.UserType))
                userMap.UserType = userDTO.UserType;


            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong with the updating the user :(");
                return StatusCode(500, ModelState);

            }
            return Ok("Success");
        }

        [HttpDelete("delete")]
        public IActionResult DeleteUser(string userId)
        {
            if(userId == null || !_userRepository.UserExists(userId))
            { return NotFound(); }

            var userToDelete = _userRepository.GetUserById(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //TODO: Add relation validation (eg: user-orders, user-adresses)

            userToDelete.IsDeleted = true;

            if (!_userRepository.UpdateUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong with the deletion of the user :(");
            }

            return Ok("Success");

        }
    }
}
