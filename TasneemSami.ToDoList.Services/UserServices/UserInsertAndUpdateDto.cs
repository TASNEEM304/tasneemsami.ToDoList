using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasneemSami.ToDoList.Services.UserServices
{
    public class UserInsertAndUpdateDto
    {
       

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }



        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [Range(1, 99)]


        public int Role { get; set; }
    }
}
