using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(20)]
        public string? ZipCode { get; set; }

        [StringLength(100)]
        public string? County { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [StringLength(30)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Region { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
