// Models/UserProfile.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineLibraryApi.Models
{
    [Table("UserProfiles")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }



        public int? UserId { get; set; }

        public User? User { get; set; }

   
    }
}