using OnlineLibraryApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineLibraryApi.Models
{
    public enum BorrowStatus
    {
        Borrowed = 1,
        Returned = 2,
    }

    [Table("Borrowings")]
    public class Borrowing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BorrowId { get; set; }

        // Navigation properties - nullable لتجنب مشاكل cycles
        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public Book? Book { get; set; }

        // Foreign Keys - Required لتجنب خطأ "The field is required"
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [JsonIgnore]
        public BorrowStatus Status { get; set; }
    }
}
