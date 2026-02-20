using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        // Navigation Properties
        public ICollection<StaffSkill> StaffSkills { get; set; } = new List<StaffSkill>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class StaffSkill
    {
        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        public int SkillId { get; set; }
        public Skill Skill { get; set; } = null!;

        public int CertificationLevel { get; set; } = 1; // 1=Basic, 2=Intermediate, 3=Advanced

        public DateTime? CertificationDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
