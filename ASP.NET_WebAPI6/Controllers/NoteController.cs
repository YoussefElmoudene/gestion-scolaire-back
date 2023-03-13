using gestion_scolaire.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace gestion_scolaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly DBContext DBContext;
        private StudentController studentController;
        private ModuleController moduleController;

        public NoteController(DBContext DBContext)
        {
            this.DBContext = DBContext;
            this.studentController = new StudentController(DBContext);
            this.moduleController = new ModuleController(DBContext);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Note>>> Get()
        {
            var List = await DBContext.Notes.Select(
                s => new Note
                {
                    Id = s.Id,
                    StudentNote = s.StudentNote,
                    Module = s.Module,
                    ModuleId = s.ModuleId,
                    Student = s.Student,
                    StudentId = s.StudentId
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
        public async Task<ActionResult<Note>> GetById(int Id)
        {
            Note note = await DBContext.Notes.Select(
                    s => new Note
                    {
                        Id = s.Id,
                        StudentNote = s.StudentNote,
                        Module = s.Module,
                        ModuleId = s.ModuleId,
                        Student = s.Student,
                        StudentId = s.StudentId
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (note == null)
            {
                return NotFound();
            }
            else
            {
                return note;
            }
        }

        [HttpPost("Add")]
        public async Task<Note> Insert(Note note)
        {
            var entity = new Note()
            {
                Id = note.Id,
                Module = null,
                Student = null,
                ModuleId = note.ModuleId,
                StudentId= note.StudentId,
                StudentNote = note.StudentNote,
            };
            DBContext.Notes.Add(entity);
            await DBContext.SaveChangesAsync();
            entity.Student = studentController.GetById(note.StudentId).Result.Value;
            entity.Module = moduleController.GetById(note.ModuleId).Result.Value;
            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> Update(Note note)
        {
            var entity = await DBContext.Notes.FirstOrDefaultAsync(s => s.Id == note.Id);
            entity.Module = null;
            entity.Student = null;
            entity.ModuleId = note.ModuleId;
            entity.StudentNote = note.StudentNote;
            entity.StudentId = note.StudentId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new Note()
            {
                Id = Id
            };
            DBContext.Notes.Attach(entity);
            DBContext.Notes.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
