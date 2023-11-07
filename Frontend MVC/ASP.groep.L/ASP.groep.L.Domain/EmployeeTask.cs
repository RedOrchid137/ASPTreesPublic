using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.groep.L.Domain
{
    public enum Priority { Low, Medium, High }
    public enum Status { Todo, In_Progress, Done }
    [Table("EmployeeTasks")]
    [Index(nameof(WorkScheduleId), IsUnique = false)]
    [Index(nameof(ZoneId), IsUnique = false)]
    public class EmployeeTask
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime scheduledStart { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime scheduledStop { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime? actualStart { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? actualStop { get; set; }

        [ForeignKey("WorkSchedule")]
        public virtual int WorkScheduleId { get; set; }
        public virtual WorkSchedule WorkSchedule { get; set; }
        
        [ForeignKey("Zone")]
        public virtual int ZoneId { get; set; }
        public virtual Zone Zone { get; set; }

        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
