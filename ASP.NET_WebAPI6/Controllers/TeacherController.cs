using gestion_scolaire.DTO;
using gestion_scolaire.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace gestion_scolaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly DBContext DBContext;
        private SpecialiteController specialiteController;

        public TeacherController(DBContext DBContext)
        {
            this.DBContext = DBContext;
            this.specialiteController = new SpecialiteController(DBContext);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Teacher>>> Get()
        {
            var List = await DBContext.Teachers.Select(
                s => new Teacher
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Cin = s.Cin,
                    Password = s.Password,
                    IsEnabled = s.IsEnabled,
                    Role = s.Role,
                    Created = s.Created,
                    Specialite = s.Specialite,
                    SpecialiteId = s.SpecialiteId,
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
        public async Task<ActionResult<Teacher>> GetById(int Id)
        {
            Teacher teacher = await DBContext.Teachers.Select(
                    s => new Teacher
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Email = s.Email,
                        Cin = s.Cin,
                        Password = s.Password,
                        IsEnabled = s.IsEnabled,
                        Role = s.Role,
                        Created = s.Created,
                        Specialite = s.Specialite,
                        SpecialiteId = s.SpecialiteId,
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (teacher == null)
            {
                return NotFound();
            }
            else
            {
                return teacher;
            }
        }

        [HttpPost("Add")]
        public async Task<Teacher> Insert(Teacher teacher)
        {
            var entity = new Teacher()
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Email = teacher.Email,
                Cin = teacher.Cin,
                Password = teacher.Password,
                IsEnabled = teacher.IsEnabled,
                Role = teacher.Role,
                Created = teacher.Created,
                Age = teacher.Age,
                Specialite = null,
                SpecialiteId = teacher.SpecialiteId,
            };
            DBContext.Teachers.Add(entity);
            await DBContext.SaveChangesAsync();
            entity.Specialite = specialiteController.GetById(teacher.SpecialiteId).Result.Value;
            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> Update(Teacher teacher)
        {
            var entity = await DBContext.Teachers.FirstOrDefaultAsync(s => s.Id == teacher.Id);
            entity.Name = teacher.Name;
            entity.Cin = teacher.Cin;
            entity.Email = teacher.Email;
            entity.Age = teacher.Age;
            entity.Password = teacher.Password;
            entity.Created = teacher.Created;
            entity.Role = teacher.Role;
            entity.Specialite = null;
            entity.SpecialiteId = teacher.SpecialiteId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new Teacher()
            {
                Id = Id
            };
            DBContext.Teachers.Attach(entity);
            DBContext.Teachers.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
