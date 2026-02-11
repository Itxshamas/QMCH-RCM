using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class ClientType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string ShortDescription { get; set; } = string.Empty;
    }
}