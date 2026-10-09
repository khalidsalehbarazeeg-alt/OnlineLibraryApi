using OnlineLibraryApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineLibraryApi.Models
{
    [Table("BookAuthors")]
    public class BookAuthor
    {
        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [JsonIgnore]
        public Book? Book { get; set; }
        [JsonIgnore]
        public Author? Author { get; set; }
    }
}