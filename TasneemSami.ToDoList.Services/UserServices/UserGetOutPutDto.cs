using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasneemSami.ToDoList.Services.UserServices
{
    public class UserGetOutPutDto
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }



        [Required]
        [MaxLength(255)]
        public string PASSWORD { get; set; }

        [Required]
        [Range(1, 99)]


        public int ROLE { get; set; }

    }
}
