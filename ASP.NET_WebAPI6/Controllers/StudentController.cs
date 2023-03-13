using gestion_scolaire.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace gestion_scolaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DBContext DBContext;
        private GroupeController GroupeController;

        public StudentController(DBContext DBContext)
        {
            this.DBContext = DBContext;
            this.GroupeController = new GroupeController(DBContext);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Student>>> Get()
        {
            var List = await DBContext.Students.Select(
                s => new Student
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Cin = s.Cin,
                    Password = s.Password,
                    IsEnabled = s.IsEnabled,
                    Role = s.Role,
                    Created = s.Created,
                    Groupe = s.Groupe,
                    GroupeId = s.GroupeId,
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Student>> GetById(int Id)
        {
            Student student = await DBContext.Students.Select(
                    s => new Student
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Email = s.Email,
                        Cin = s.Cin,
                        Password = s.Password,
                        IsEnabled = s.IsEnabled,
                        Role = s.Role,
                        Created = s.Created,
                        Groupe = s.Groupe,
                        GroupeId= s.GroupeId,
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return student;
            }
        }

        [HttpPost("Add")]
        public async Task<Student> Insert(Student student)
        {
            var entity = new Student()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Cin = student.Cin,
                Age = student.Age,
                Password = student.Password,
                IsEnabled = student.IsEnabled,
                Role = student.Role,
                Created = student.Created,
                Groupe = student.Groupe,
                GroupeId = student.GroupeId,
            };

            DBContext.Students.Add(entity);
            await DBContext.SaveChangesAsync();
            entity.Groupe = GroupeController.GetById(student.GroupeId).Result.Value;
            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> Update(Student student)
        {
            var entity = await DBContext.Students.FirstOrDefaultAsync(s => s.Id == student.Id);
            entity.Name = student.Name;
            entity.Cin = student.Cin;
            entity.Email = student.Email;
            entity.Password = student.Password;
            entity.Age = student.Age;
            entity.Created = student.Created;
            entity.Role = student.Role;
            entity.Groupe = student.Groupe;
            entity.GroupeId = student.GroupeId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new Student()
            {
                Id = Id
            };
            DBContext.Students.Attach(entity);
            DBContext.Students.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
