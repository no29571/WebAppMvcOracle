using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppMvc.Validation;

namespace WebAppMvc.Models
{
    [Table("student")]
    public class Student : IHasTimestamps
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Column("info")]
        [MaxLength(100)]
        [MaxByteLength(10)]
        public string? Info { get; set; }

        /* ApplicationDbContextでリレーションを定義 */
        [Column("dept1_id")]
        public string? Department1Id { get; set; }
        //[ForeignKey("dept1_id")]
        public Department? Department1 { get; set; }

        [Column("dept2_id")]
        public string? Department2Id { get; set; }
        //[ForeignKey("dept2_id")]
        public Department? Department2 { get; set; }

        /* IHasTimestamps */
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
