using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Entities
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Password { get; set; } = null!;
    }
}
