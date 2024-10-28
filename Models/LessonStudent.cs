using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMvc.Models
{
    //[PrimaryKey(nameof(LessonId), nameof(StudentId))]// or Fluent API（ApplicationDbContext）
    [Table("lesson_student")]
    public class LessonStudent : IHasTimestamps
    {
        /* ApplicationDbContextで複合主キーを定義） */
        [Column("lesson_id")]
        [Remote(action: "UniqueKey", controller: "LessonStudents", AdditionalFields = nameof(StudentId))]
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }

        [Column("student_id")]
        [Remote(action: "UniqueKey", controller: "LessonStudents", AdditionalFields = nameof(LessonId))]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

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
