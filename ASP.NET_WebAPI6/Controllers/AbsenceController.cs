using gestion_scolaire.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace gestion_scolaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly DBContext DBContext;
        private ModuleController moduleController;
        private StudentController studentController;
        private GroupeController groupeController;

        public AbsenceController(DBContext DBContext)
        {
            this.DBContext = DBContext;
            this.moduleController = new ModuleController(DBContext);
            this.studentController = new StudentController(DBContext);
            this.groupeController = new GroupeController(DBContext);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Absence>>> Get()
        {
            var List = await DBContext.Absences.Select(
                s => new Absence
                {
                    Id = s.Id,
                    Date = s.Date,
                    Module = s.Module,
                    ModuleId = s.ModuleId,
                    Student = s.Student,
                    StudentId = s.StudentId,
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
        public async Task<ActionResult<Absence>> GetById(int Id)
        {
            Absence sp = await DBContext.Absences.Select(
                    s => new Absence
                    {
                        Id = s.Id,
                        StudentId= s.StudentId,
                        Date= s.Date,
                        Module = s.Module,
                        ModuleId = s.ModuleId ,
                        Student = s.Student,
                        Groupe = s.Groupe,
                        GroupeId = s.GroupeId,
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (sp == null)
            {
                return NotFound();
            }
            else
            {
                return sp;
            }
        }

        [HttpPost("Add")]
        public async Task<Absence> Insert(Absence s)
        {
            var entity = new Absence()
            {
                Id = s.Id,
                StudentId = s.StudentId,
                Date = s.Date,
                Module = s.Module,
                ModuleId = s.ModuleId,
                GroupeId = s.GroupeId,
                Student = s.Student
            };
            DBContext.Absences.Add(entity);
            await DBContext.SaveChangesAsync();
            entity.Module = moduleController.GetById(s.ModuleId).Result.Value;
            entity.Student = studentController.GetById(s.StudentId).Result.Value;
            entity.Groupe = groupeController.GetById(s.GroupeId).Result.Value;
            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> Update(Absence absence)
        {
            var entity = await DBContext.Absences.FirstOrDefaultAsync(s => s.Id == absence.Id);
            entity.Date = absence.Date;
            entity.Module = absence.Module;
            entity.ModuleId = absence.ModuleId;
            entity.Student = absence.Student;
            entity.StudentId = absence.StudentId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new Absence()
            {
                Id = Id
            };
            DBContext.Absences.Attach(entity);
            DBContext.Absences.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
