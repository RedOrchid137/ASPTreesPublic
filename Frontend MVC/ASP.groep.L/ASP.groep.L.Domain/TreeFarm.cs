using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.groep.L.Domain
{
    [Table("TreeFarms")]
    public class TreeFarm
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        public ICollection<Site>? Sites { get; set; }
    }
}
