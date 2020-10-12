using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MariusTodoList.Models
{
    public class TasksModel
    {
        [Key]
        public int TaskID { get; set; }
        public int WeekDay { get; set; }
        public string Description { get; set; }
        public DateTime? DtCreated { get; set; }
        public DateTime? DtUpdated { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public bool IsDone { get; set; }
        public int? UserID { get; set; }

        public virtual ApplicationUser User { get; set; }


    }
}
