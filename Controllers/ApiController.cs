using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ogrenci_sistemi.Models;
using System.Reflection;

namespace ogrenci_sistemi.Controllers
{
    public class ApiController : Controller
    {
        string connectionString = "Server=X;Initial Catalog=X;User Id =X; Password =X;TrustServerCertificate=X";

        [HttpGet]
        [Route("/api")]
        public IActionResult Api()
        {
            using var connection = new SqlConnection(connectionString);

            var sql = "SELECT * FROM ogrenciStudents ORDER BY id DESC";
            var students = connection.Query<Student>(sql).ToList();
            return Json(students);
        }

        [HttpPost]
        [Route("/api")]
        public IActionResult AddApi([FromBody]Student model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var connection = new SqlConnection(connectionString);

            var newRecordId = connection.ExecuteScalar<int>("INSERT INTO ogrenciStudents (name,surname,age,email,phoneNumber) VALUES (@Name,@Surname,@Age,@Email,@PhoneNumber) SELECT SCOPE_IDENTITY()",model);
            model.Id = newRecordId;

            return Ok(new { Message = $"{model.Name} {model.Surname} adlı {model.Id} numaralı öğrenci başarı ile eklendi!" });
        }

        [HttpDelete]
        [Route("/api/{id}")]
        public IActionResult DeleteApi(int id)
        {
            using var connection = new SqlConnection(connectionString);

            var rowsAffected = connection.Execute("DELETE FROM ogrenciStudents WHERE id = @Id", new { Id = id });

            if (rowsAffected == 0)
            {
                return NotFound(new { Message = "Böyle bir öğrenci id bulunamadı!" });
            }

            return Ok(new { Message = $"{id} numaralı öğrenci başarı ile silindi!" });
        }

        [HttpPatch]
        [Route("/api/{id}")]
        public IActionResult UpdateApi([FromBody]Student model, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var connection = new SqlConnection(connectionString);
            var sql = "UPDATE ogrenciStudents SET name = @Name, surname = @Surname, age = @Age, Email = @Email, phoneNumber = @PhoneNumber WHERE id = @Id;";
            var data = new
            {
                Id = id,
                Name = model.Name,
                Surname = model.Surname,
                Age = model.Age,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var rowsAffected = connection.Execute(sql, data);

            if (rowsAffected == 0)
            {
                return BadRequest(new { Message = "Böyle bir öğrenci id bulunamadı!" });
            }

            return Ok(new { Message = $"Öğrenci başarı ile güncellendi!" });
        }
    }
}
