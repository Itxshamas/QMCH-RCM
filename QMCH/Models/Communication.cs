using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string SenderId { get; set; } = string.Empty;
        public User Sender { get; set; } = null!;

        public string RecipientId { get; set; } = string.Empty;
        public User Recipient { get; set; } = null!;

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public DateTime SentAt { get; set; } = DateTime.Now;

        public DateTime? ReadAt { get; set; }

        [StringLength(50)]
        public string Priority { get; set; } = "Normal"; // Low, Normal, High, Urgent
    }

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [StringLength(50)]
        public string Type { get; set; } = "Info"; // Info, Warning, Alert, Success

        public bool IsRead { get; set; } = false;

        [StringLength(500)]
        public string? ActionLink { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ReadAt { get; set; }
    }
}
