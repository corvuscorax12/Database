using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class SemesterModel
    {
        [Key]
        public int id { get; set; }
        public string semester_name { get; set; }
    }

}
