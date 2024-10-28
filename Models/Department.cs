using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMvc.Models
{
    [Table("department")]
    public class Department : IHasTimestamps
    {
        [Column("id")]
        [MaxLength(5)]
        [RegularExpression(@"[a-zA-Z0-9]+")]//, ErrorMessage = "半角英数字のみ"
        [Remote(action: "UniqueKey", controller: "Departments")]
        public string Id { get; set; }

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

        /* ApplicationDbContextでリレーションを定義 */
        public ICollection<Student>? Students1 { get; }
        public ICollection<Student>? Students2 { get; }
    }
}
