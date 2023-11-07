using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.groep.L.Domain
{
    [Table("Zones")]
    [Index(nameof(TreeSpeciesId), IsUnique = false)]
    [Index(nameof(SiteId), IsUnique = false)]
    public class Zone
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }
        public double? SurfaceArea { get; set; }

        [ForeignKey("Site")]
        [Required]
        public virtual int SiteId { get; set; }
        public virtual Site Site { get; set; }

        [ForeignKey("TreeSpecies")]
        //[Required]
        public virtual int TreeSpeciesId { get; set; }
        public virtual TreeSpecies TreeSpecies { get; set; }

        public virtual ICollection<EmployeeTask> EmployeeTasks { get; set; }
    }
}
