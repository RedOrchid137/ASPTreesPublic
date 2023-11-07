using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.groep.L.Domain {
    public enum Role { MVC_Admin, MVC_Planner, Ionic_Employee }
    public class User
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
        [StringLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

        //[Required]
        [Display(Name = "FirstName")]
        [StringLength(255)]
        public string? FirstName { get; set; }

        //[Required]
        [StringLength(255)]
        public string? LastName { get; set; }

        [NotMapped]
        public string Fullname
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
        public ICollection<WorkSchedule>? EmployeeWorkSchedules { get; set; }
        public ICollection<WorkSchedule>? PlannerWorkSchedules { get; set; }
    }
}
