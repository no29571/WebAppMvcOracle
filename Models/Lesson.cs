using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMvc.Models
{
    [Table("lesson")]
    public class Lesson : IHasTimestamps, IValidatableObject
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

        /* IValidatableObject */
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Info == null)
            {
                //フィールドの検証結果として出力
                yield return new ValidationResult(
                    "[IValidatableObject] Required.",
                    new[] { nameof(Info) });
            }
            if (Info != null && Info.Length < Name.Length)
            {
                //モデルの検証結果として出力
                yield return new ValidationResult("[IValidatableObject] Info.Length < Name.Length");
            }
        }
    }
}
