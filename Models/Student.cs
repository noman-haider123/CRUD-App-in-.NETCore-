using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noman.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("StudentName",TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column("StudentDescription",TypeName = "varchar(max)")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The Age is Required")]
        public int? Age { get; set; }
        [Column("StudentImage",TypeName = "varchar(255)")]
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? UploadImage { get; set; }
    }
}
