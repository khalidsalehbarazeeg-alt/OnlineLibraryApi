using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineLibraryApi.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength (12)]
        public string Password { get; set; }

        [MaxLength(20)]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        [JsonIgnore]
        public UserProfile? Profile { get; set; }


        [JsonIgnore]
        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}