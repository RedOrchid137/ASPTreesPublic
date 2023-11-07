using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.groep.L.Domain
{
    public enum Season { Autumn, Winter, Spring, Summer }
    [Table("MaintenancePlans")]
    public class MaintenancePlan
    {


        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        //[Required]
        public string Title { get; set; }

        [EnumDataType(typeof(Season))]
        public Season Season { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        [NotMapped]
        [Display(Name = "Upload Maintenance Plan")]
        [DataType(DataType.Upload)]
        public IFormFile? PlanFile { get; set; }

        public string? FileName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public string? QRCodeName { get; set; }
        public string? QRCode { get; set; }

        public TreeSpecies TreeSpecies { get; set; }
    }
}
