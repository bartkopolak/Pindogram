using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using pindogramApp.Dtos;
using pindogramApp.Entities;
using pindogramApp.Helpers;
using pindogramApp.Services.Interfaces;

namespace pindogramApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IGroupService groupService)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _groupService = groupService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Niepoprawny login albo hasło" });
            if (user.Group == null)
                return BadRequest(new { message = $"Użytkownik {user.FirstName} nie jest przypisany do rzadnej z grup. Skontaktuj się z administratorem" });
            if (!user.IsActive)
                return BadRequest(new { message = "Konto oczekuje na aktywację" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(user.Group.Name,"")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString,
                Group = user.Group.Name
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);
            try
            {
                // save 
                _userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Policy = "ADMIN")]
        [HttpPost("[Action]/{userId}")]
        public IActionResult AddUserToUserGroup(int userId)
        {
            _userService.AddUserToUserGroup(userId);

            return Ok(new { message = "Użytkownik został dodany do grupy 'USER'" });
        }

        [Authorize(Policy = "ADMIN")]
        [HttpPost("[Action]/{userId}")]
        public IActionResult AddUserToAdminGroup(int userId)
        {
            _userService.AddUserToAdminGroup(userId);

            return Ok(new { message = "Użytkownik został dodany do grupy 'ADMIN'" });
        }

        [Authorize(Policy = "ADMIN")]
        [HttpPost("[Action]/{Id}")]
        public IActionResult ActiveUser(int Id)
        {
            var user = _userService.ActiveUser(Id);
            var userDtos = _mapper.Map<UserDto>(user);

            return Ok(userDtos);
        }

        [Authorize(Policy = "ADMIN")]
        [HttpGet("[Action]")]
        public IActionResult GetAllUnactivatedUsers()
        {
            var users = _userService.GetAllUnactivatedUsers();
            var usersDtos = _mapper.Map<IList<UserDto>>(users);
            if(usersDtos == null)
                return Ok(new { message = "Nie ma nie zaakceptowanych użytkwoników'" });

            return Ok(usersDtos);
        }

        [Authorize(Policy = "ADMIN")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                // save 
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

    }
}