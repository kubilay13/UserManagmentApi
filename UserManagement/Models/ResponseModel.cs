using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class ResponseModel
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? SurName { get; set; }

        public bool UserStatus { get; set; }

        public int Age { get; set; }

        [MaxLength(11)]
        public string? TcNo { get; set; }

        [MaxLength(11)]
        public string? PhoneCode { get; set; }

        [MaxLength(10)]
        public string? PhoneNo { get; set; }

        public string? Password { get; set; }
    }
}
