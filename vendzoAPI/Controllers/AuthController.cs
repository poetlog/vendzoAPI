using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using vendzoAPI.DTO;
using AutoMapper;
using vendzoAPI.Interfaces;


namespace vendzoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IConfiguration configuration,
                              IUserRepository userRepository,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
            if(_userRepository.UserExistsByEmail(user.Email))
            {
                return BadRequest(new { Error = "Email already exists" });
            }
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var userMap = _mapper.Map<User>(model);
            userMap.CreatedAt = DateTime.Now;
            userMap.LoginId = user.Id;
            userMap.Username = model.Username;
            userMap.IsClient = model.IsClient;

            if ( !_userRepository.CreateUser(userMap))
                return BadRequest("Could not create user.");
            
            return Ok(new { Result = "Registration successful" });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded &&
                _userRepository.GetUserByUsername(model.Username) != null &&
                !_userRepository.GetUserByUsername(model.Username).IsDeleted)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var userId = _userRepository.GetUserByUsername(model.Username).Id;
                var token = GenerateJwtToken(user);

                return Ok(new { Token = token,
                                UserId = userId
                               });
            }

            return Unauthorized();
        }

        [HttpPut("changePassword/userId={userId}")]
        public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordDTO model)
        {
            var user = await _userManager.FindByIdAsync(_userRepository.GetUserById(userId).LoginId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                return BadRequest(changePasswordResult.Errors);
            }

            return Ok(new { Result = "Password change successful" });
        }



        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(300),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
