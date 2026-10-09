// Models/Book.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineLibraryApi.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]

        public int PublishYear { get; set; }

        [MaxLength(20)]
        public string ISBN { get; set; }

        [Required]

        public int TotalCopies { get; set; } = 1;

        [Required]
        [Range(0, 1000)]
        public int AvailableCopies { get; set; } = 1;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public ICollection<BookAuthor>? BookAuthors { get; set; }


        [JsonIgnore]
        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}