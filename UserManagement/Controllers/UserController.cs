using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.AppDbContext;
using UserManagement.Models;
using System.Security.Cryptography;
using System.Text;


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
        public async Task<IActionResult> AddUser([FromBody] UpdateUserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest("User creation failed");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

               
                var newUser = new ResponseModel
                {
                    UserName = userDto.UserName,
                    SurName = userDto.SurName,
                    UserStatus = userDto.UserStatus,
                    Age = userDto.Age,
                    TcNo = userDto.TcNo,
                    PhoneCode = userDto.PhoneCode,
                    PhoneNo = userDto.PhoneNo,
                    Password = hashedPassword 
                };

                await _context.ResponseModels.AddAsync(newUser);
                await _context.SaveChangesAsync();

                var responseDto = new
                {
                    newUser.UserName,
                    newUser.SurName,
                    newUser.UserStatus,
                    newUser.Age,
                    newUser.TcNo,
                    newUser.PhoneCode,
                    newUser.PhoneNo
                };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UpdateUserDto model)
        {
            try
            {
               
                var user = await _context.ResponseModels.FirstOrDefaultAsync(u => u.UserName == model.UserName);

                if (user == null)
                {
                    return BadRequest("Kullanıcı adı veya şifre hatalı.");
                }

               
                if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                   
                    return BadRequest("Kullanıcı adı veya şifre hatalı.");
                }

                return Ok("Giriş başarılı!");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"İç sunucu hatası: {ex.Message}");
            }
        }


        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        [HttpGet("GetAllUser")]
        public async Task<List<object>> GetAllUser()
        {
            var users = await _context.ResponseModels.ToListAsync();
            return users.Select(q => new
            {
                q.UserName,
                q.SurName,
                q.UserStatus,
                q.Age,
                q.TcNo,
                q.PhoneCode,
                q.PhoneNo,
               
            }).ToList<object>();
        }



        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.ResponseModels.Find(id);

            if (user == null)
            {
                return NotFound("User Not Found.");
            }
            var response = new
            {
                user.UserName,
                user.SurName,
                user.UserStatus,
                user.Age,
                user.TcNo,
                user.PhoneCode,
                user.PhoneNo,
              
            };
            return Ok(response);
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
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updatedUserDto)
        {
            var userToUpdate = await _context.ResponseModels.FirstOrDefaultAsync(u => u.Id == id);
            if (userToUpdate == null)
            {
                return NotFound("User Not Found.");
            }


            userToUpdate.UserName = updatedUserDto.UserName;
            userToUpdate.SurName = updatedUserDto.SurName;
            userToUpdate.UserStatus = updatedUserDto.UserStatus;
            userToUpdate.Age = updatedUserDto.Age;
            userToUpdate.TcNo = updatedUserDto.TcNo;
            userToUpdate.PhoneCode = updatedUserDto.PhoneCode;
            userToUpdate.PhoneNo = updatedUserDto.PhoneNo;
            userToUpdate.Password = updatedUserDto.Password;

            _context.ResponseModels.Update(userToUpdate);
            await _context.SaveChangesAsync();

          
            return Ok(new
            {
                userToUpdate.UserName,
                userToUpdate.SurName,
                userToUpdate.UserStatus,
                userToUpdate.Age,
                userToUpdate.TcNo,
                userToUpdate.PhoneCode,
                userToUpdate.PhoneNo,
              
            });
        }
      


    }

    
}
