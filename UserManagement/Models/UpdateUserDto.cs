using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class UpdateUserDto
    {
        [MaxLength(15, ErrorMessage = "Kullanıcı adı en fazla 15 karakter olabilir.")]
        public string? UserName { get; set; }

        [MaxLength(15, ErrorMessage = "Soyadı en fazla 15 karakter olabilir.")]
        public string? SurName { get; set; }

        public bool UserStatus { get; set; }

        [Range(0, 99, ErrorMessage = "Yaşınız 0-99 arasında olmalıdır.")]
        public int Age { get; set; }

        [MaxLength(11, ErrorMessage = "TC kimlik numarası 11 haneli olmalıdır.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC kimlik numarası 11 rakamdan oluşmalıdır.")]
        public string? TcNo { get; set; }

        [MaxLength(6, ErrorMessage = "Telefon kodu en fazla 6 karakter olabilir.")]
        [RegularExpression(@"^\d{1,6}$", ErrorMessage = "Telefon kodu sadece rakam içermelidir.")]
        public string? PhoneCode { get; set; }

        [MaxLength(10, ErrorMessage = "Telefon numarası en fazla 10 karakter olabilir.")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "Telefon numarası sadece rakam içermelidir.")]
        public string? PhoneNo { get; set; }

        [MinLength(7, ErrorMessage = "Şifre en az 7 karakter olmalıdır.")]
        public string? Password { get; set; }
    }
}
