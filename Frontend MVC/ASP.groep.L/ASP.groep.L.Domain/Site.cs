using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.groep.L.Domain
{
    [Table("Sites")]
    [Index(nameof(TreeFarmId), IsUnique = false)]
    [Index(nameof(AddressId), IsUnique = false)]
    public class Site
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        [StringLength(255)]
        public string? BluePrint { get; set; }

        [StringLength(255)]
        public string? BluePrintName { get; set; }

        [NotMapped]
        [Display(Name = "Upload Blueprint")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        [ForeignKey("TreeFarm")]
        [Required]
        public virtual int TreeFarmId { get; set; }
        public virtual TreeFarm TreeFarm { get; set; }

        [ForeignKey("Address")]
        [Required]
        public virtual int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public ICollection<Zone> Zones { get; set; }
    }
}
