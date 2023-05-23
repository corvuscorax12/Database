using Database.DAL;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Database.Controllers
{
    public class RosterListController : Controller
    {
        public IActionResult Index()
        {

            NpgsqlConnection conn = new NpgsqlConnection(ArchivalContext.conn);
            List<SemesterView> semesterViews = new List<SemesterView>();
            NpgsqlCommand command = new NpgsqlCommand("Select * from public.viewroster", conn);
            conn.Open();
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                semesterViews.Add(new()
                {
                    roster_id = reader.GetInt32(0),
                    first_name = (string)reader[1],
                    last_name = (string)reader[2],
                    role_name = (string)reader[3],
                    semester_name = (string)reader[4]
                }
                   );
                    

            }
            return View(semesterViews);
        }
    }
}
