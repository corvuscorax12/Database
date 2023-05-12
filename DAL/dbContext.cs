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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("host='127.0.0.1'; port=5432 ;DataBase='archival'; user id=postgres;");
        public DbSet<StaffModel> Staff { get; set; }

    }
   

}
