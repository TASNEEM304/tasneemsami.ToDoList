using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasneemSami.ToDoList.Database.Models
{
    [Table(name: "Tasks")]
    public class Tasks
    {
        [Key]
        [Required]
        public int ID { get; set; }


        [Required(ErrorMessage = "TITLE Is Requierd")]
        [StringLength(250)]
        public string TITLE { get; set; }


        [Required(ErrorMessage = "DESCRIPTION Is Requierd")]
        [StringLength(250)]
        public string DESCRIPTION { get; set; }

        [Required]
       
        public bool ISCOMPLETED { get; set; } = false;

        [Required(ErrorMessage = "DUEDATE Is Requierd")]
     
        public DateTime DUEDATE { get; set; }


        [Required(ErrorMessage = "CATEGORY Is Requierd")]
        [Range(1,99)]
        
        public int CATEGORY { get; set; }

        
        [Required(ErrorMessage = "PRIORITY Is Requierd ")]
        [Range(1, 99)]
     
        public int PRIORITY { get; set; } 

        
        [Required]
        
        public int USERID { get; set; }

        [ForeignKey("USERID")]
        public  Users User { get; set; }
    }
}
