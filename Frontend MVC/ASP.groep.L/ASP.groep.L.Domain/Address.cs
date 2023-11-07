using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.groep.L.Domain
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string? StreetName { get; set; }

        [StringLength(100)]
        public string? Commune { get; set; }

        [StringLength(4)]
        public string? ZipCode { get; set; }

        [StringLength(3)]
        public string? HouseNr { get; set; }

        public virtual Site Site { get; set; }
    }
}
