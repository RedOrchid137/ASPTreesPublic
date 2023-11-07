using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.groep.L.Domain
{
    [Index(nameof(PlannerId), IsUnique = false)]
    [Index(nameof(EmployeeId), IsUnique = false)]
    [Table("WorkSchedules")]
    public class WorkSchedule
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        //[Required]
        public virtual int EmployeeId { get; set; }
        public virtual User Employee { get; set; }

        [ForeignKey("Planner")]
        //[Required]
        public virtual int PlannerId { get; set; }
        public virtual User Planner { get; set; }

        public virtual ICollection<EmployeeTask>? EmployeeTasks { get; set; }
        public string? Description { get; set; }

    }
}
