using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.BL.DTOs.AccountDTOs
{
    public class RegisterDto
    {
        [Required, MaxLength(64), MinLength(3, ErrorMessage = "Minimum 3 simvol daxil edilmelidir !")]
        public string FullName { get; set; }
        [Required, MaxLength(256), MinLength(1, ErrorMessage = "Minimum 1 simvol daxil edilmelidir !")]
        public string UserName { get; set; }
        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string RePassword { get; set; }
        public IFormFile ProfileImage {  get; set; }
        
        public string? PhoneNumber {  get; set; }
    }
}
