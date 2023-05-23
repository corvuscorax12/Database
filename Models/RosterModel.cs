using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class RosterModel
    {
        [Key]
        public int roster_id { get; set; }
        public int semester_id { get; set; }
        public int staff_id { get; set; }
        public int role_id { get; set; }


    }
    public class RoleModel
    {
        [Key]
        public int role_id { get; set;}
        public string role_name { get; set; }
    }
    public class SemesterView
    {
        public int roster_id { get; set;}
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string role_name { get; set; }
        public string semester_name { get; set; }
    }
}
