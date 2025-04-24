using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasneemSami.ToDoList.Services.TaskServices
{
    public class TaskInsertAndUpdateDto
    {
        [Key]
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "TITLE Is Requierd")]
        [StringLength(250)]
        public string Title { get; set; }


        [Required(ErrorMessage = "DESCRIPTION Is Requierd")]
        [StringLength(250)]
        public string Description { get; set; }

        [Required]

        public bool IsCompleted { get; set; } = false;

        [Required(ErrorMessage = "DUEDATE Is Requierd")]

        public DateTime DueDate { get; set; }


        [Required(ErrorMessage = "CATEGORY Is Requierd")]
        [Range(1, 99)]

        public int Category { get; set; }


        [Required(ErrorMessage = "PRIORITY Is Requierd ")]
        [Range(1, 99)]

        public int Priority { get; set; }


        [Required]

        public int UserId { get; set; }
        
    }
}
