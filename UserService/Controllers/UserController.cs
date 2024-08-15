// Controllers/UserController.cs
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOS;
using UserService.Helpers;
using UserService.Models;
using UserService.ServiceLayer;

namespace UserService.Controllers
{
    [EnableCors("MyPolicy")] //Enabling CORS
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetAllUsers();
            if(users== null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation("Get called on by id :" + DateTime.Now.ToString() + Request.Host.Value);//logging

            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _logger.LogInformation("User Registered in  :" + DateTime.Now.ToString() + Request.Host.Value);//logging
            if (_userService.RegisterUser(user))
                return new OkObjectResult("User added");

            return BadRequest("User not added");
        }

        [HttpPost]
        [Route("Validate")]
        public ActionResult<TokenResult> Validate([FromBody] UserLoginDTO user)
        {
            _logger.LogInformation("User validated in :" + DateTime.Now.ToString() + Request.Host.Value);//logging
            var validatedUser = _userService.ValidateUser(user);
            if (validatedUser != null)
            {
                user.Role = validatedUser.Role; // Ensure Role is set
                return new TokenResult()
                {
                    Status = "success",
                    Token = new TokenHelper().GenerateToken(user)
                };
            }

            return new NotFoundObjectResult(new TokenResult()
            {
                Status = "failed",
                Token = null
            });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            if (id != user.UserId)
                return BadRequest();

            var result = _userService.UpdateUser(user);
            if (result)
                return Ok(new { message = "User updated successfully" });

            return BadRequest(new { message = "User update failed" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userService.DeleteUser(id);
            if (result)
                return Ok(new { message = "User deleted successfully" });

            return BadRequest(new { message = "User deletion failed" });
        }
    }
}
