using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMvc.Models
{
    [Table("lesson")]
    public class Lesson : IHasTimestamps
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column("info")]
        [MaxLength(100)]
        public string? Info { get; set; }

        /* IHasTimestamps */
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
