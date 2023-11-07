using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.groep.L.Domain
{
    [Table("TreeSpecies")]
    [Index(nameof(MaintenancePlanID), IsUnique = false)]
    public class TreeSpecies
    {
        [Key]
        [Required]
        public int Id { get; set; }

        //[Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        [StringLength(255)]
        public string? PicturePath { get; set; }

        [NotMapped]
        [Display(Name = "Upload Tree Image")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        public string? ImageName { get; set; }

        [ForeignKey("MaintenancePlan")]
        //[Required]
        public virtual int MaintenancePlanID { get; set; }
        public virtual MaintenancePlan MaintenancePlan { get; set; }

        public ICollection<Zone> Zones { get; set; }


    }
}
