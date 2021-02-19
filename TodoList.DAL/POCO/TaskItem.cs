using System.ComponentModel.DataAnnotations;

namespace TodoList.Data.POCO
{
    public class TaskItem : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Task Name")]

        public string TaskName { get; set; }
        [Display(Name = "Is completed?")]
        public bool IsCompleted { get; set; } = false;
    }
}
