using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.DTOs
{
    public class UserDTO
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Password { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
