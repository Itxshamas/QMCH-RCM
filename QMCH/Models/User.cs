using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class User : IdentityUser
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        [StringLength(100)]
        public string? Department { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
