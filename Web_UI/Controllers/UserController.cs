using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUser _user;
        public UserController(IConfiguration configuration, IUser user)
        {
            _configuration = configuration;
            _user = user;
        }

        [HttpGet("get-user-data/{inputUserId}")]
        public List<Users> GetUser(int inputUserId)
        {
            List<Users> users = new List<Users>();
            users = _user.GetUserData(inputUserId);
            
            return users;
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] Users user)
        {
            if (user == null) return BadRequest("Invalid Data");

            bool isAdded = _user.AddUser(user);

            if (isAdded) return Ok("User Added Succesfully");
            else return BadRequest("Bad Request");
        }

        [HttpPut("update-user/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("Invalid Data");
            }

            bool isUpdated = _user.UpdateUser(userId, user);

            if (isUpdated) return Ok("User Updated Successfully");
            else return NotFound("User not found");
        }

        [HttpDelete("delete-user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            MyUserBusinessCode businessCode = new MyUserBusinessCode(_configuration);
            bool isDeleted = _user.DeleteUser(userId);

            if (isDeleted) return Ok("User Deleted Successfully");
            else return NotFound("User not found");
        }


    }
}
