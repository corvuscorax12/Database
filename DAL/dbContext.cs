using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Database.DAL;
using Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Database.DAL
{


    public class ArchivalContext :DbContext
    {
        public ArchivalContext(DbContextOptions<ArchivalContext> options)
            : base(options)
        {
        }
        public static string conn = "host='127.0.0.1'; port=5432 ;DataBase='archival'; user id=postgres; Include Error Detail = true";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("host='127.0.0.1'; port=5432 ;DataBase='archival'; user id=postgres; Include Error Detail = true");
        public DbSet<StaffModel> Staff { get; set; }
        public DbSet<Database.Models.RosterModel> SemesterRoster { get; set; } = default!;
        public DbSet<Database.Models.SemesterModel> Semester { get; set; } = default!;
        public DbSet<Database.Models.RoleModel> Role { get; set; } = default!;
        public DbSet<Database.Models.IssueModel> Issue { get; set; } = default!;



    }


}
