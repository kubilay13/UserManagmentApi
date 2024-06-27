using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.AppDbContext;
using UserManagement.Models;
using System.Security.Cryptography;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _context;

        public UserController(Context context)
        {
            _context = context;
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] ResponseModel userRequest)
        {
            try
            {

                var response = new ResponseModel
                {
                    UserName = userRequest.UserName,
                    SurName = userRequest.SurName,
                    UserStatus = userRequest.UserStatus,
                    Age = userRequest.Age,
                    TcNo = userRequest.TcNo,
                    PhoneCode = userRequest.PhoneCode,
                    PhoneNo = userRequest.PhoneNo,
                    Password = userRequest.Password
                };
                if (userRequest == null)
                {
                    return BadRequest("User creation failed");
                }
                await _context.ResponseModels.AddAsync(userRequest);
                await _context.SaveChangesAsync();
                return Ok(userRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllUser")]
        public async Task<List<ResponseModel>> GetAllUser()
        {
            var users = await _context.ResponseModels.ToListAsync();
            return users.Select(q => new ResponseModel
            {
                Id = q.Id,
                UserName = q.UserName,
                SurName = q.SurName,
                UserStatus = q.UserStatus,
                Age = q.Age,
                TcNo = q.TcNo,
                PhoneCode = q.PhoneCode,
                PhoneNo = q.PhoneNo,
                Password = q.Password,

            }).ToList();
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var user = _context.ResponseModels.ToList();
            return Ok(user);
        }

        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.ResponseModels.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete("Delete_User")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.ResponseModels.Find(id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            _context.ResponseModels.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpPut("UserUpdate")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] ResponseModel updatedUser)
        {

            var userToUpdate = await _context.ResponseModels.FirstOrDefaultAsync(u => u.Id == id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.UserName = updatedUser.UserName;
            userToUpdate.SurName = updatedUser.SurName;
            userToUpdate.UserStatus = updatedUser.UserStatus;
            userToUpdate.Age = updatedUser.Age;
            userToUpdate.TcNo = updatedUser.TcNo;
            userToUpdate.PhoneCode = updatedUser.PhoneCode;
            userToUpdate.PhoneNo = updatedUser.PhoneNo;
            userToUpdate.Password = updatedUser.Password;
            _context.ResponseModels.Update(userToUpdate);
            await _context.SaveChangesAsync();
            return Ok(userToUpdate);
        }
        


    }


}
