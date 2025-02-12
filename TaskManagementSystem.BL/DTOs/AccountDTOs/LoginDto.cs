using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.BL.DTOs.AccountDTOs
{
    public class LoginDto
    {
        [Required, MaxLength(64)]
        public string UserNameOrEmail { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
