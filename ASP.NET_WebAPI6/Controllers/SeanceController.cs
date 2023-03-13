using gestion_scolaire.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Reflection;

namespace gestion_scolaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeanceController : ControllerBase
    {
        private readonly DBContext DBContext;
        private TeacherController teacherController;
        private GroupeController groupeController;
        private ModuleController moduleController;

        public SeanceController(DBContext DBContext)
        {
            this.DBContext = DBContext;
            this.teacherController = new TeacherController(DBContext);
            this.groupeController = new GroupeController(DBContext);
            this.moduleController = new ModuleController(DBContext);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Seance>>> Get()
        {
            var List = await DBContext.Seances.Select(
                s => new Seance
                {
                    Id = s.Id,
                    ModuleId = s.ModuleId,
                    TeacherId = s.TeacherId,
                    GroupId = s.GroupId,
                    AllDay = s.AllDay,
                    DaysOfWeek = s.DaysOfWeek,
                    EndTime = s.EndTime,
                    StartTime = s.StartTime,
                    Title = s.Title,
                    Groupe = s.Groupe,
                    Module = s.Module,
                    Teacher= s.Teacher,
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
        public async Task<ActionResult<Seance>> GetById(int Id)
        {
            Seance seance = await DBContext.Seances.Select(
                    s => new Seance
                    {
                        Id = s.Id,
                        ModuleId = s.ModuleId,
                        TeacherId = s.TeacherId,
                        GroupId = s.GroupId,
                        AllDay = s.AllDay,
                        DaysOfWeek = s.DaysOfWeek,
                        EndTime = s.EndTime,
                        StartTime = s.StartTime,
                        Title = s.Title,
                        Groupe = s.Groupe,
                        Module = s.Module,
                        Teacher = s.Teacher,
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (seance == null)
            {
                return NotFound();
            }
            else
            {
                return seance;
            }
        }

        [HttpPost("Add")]
        public async Task<Seance> Insert(Seance seance)
        {
            var entity = new Seance()
            {
                Id = seance.Id,
                AllDay=seance.AllDay,
                DaysOfWeek=seance.DaysOfWeek,
                StartTime = seance.StartTime,
                EndTime = seance.EndTime,
                TeacherId=seance.TeacherId,
                Title = seance.Title,
                GroupId = seance.GroupId,
                ModuleId = seance.ModuleId,
                

            };
            DBContext.Seances.Add(entity);
            await DBContext.SaveChangesAsync();
            entity.Module = moduleController.GetById(seance.ModuleId).Result.Value;
            entity.Teacher = teacherController.GetById(seance.TeacherId).Result.Value;
            entity.Groupe = groupeController.GetById(seance.GroupId).Result.Value;
            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> Update(Seance seance)
        {
            var entity = await DBContext.Seances.FirstOrDefaultAsync(s => s.Id == seance.Id);
            entity.Id = seance.Id;
            entity.AllDay = seance.AllDay;
            entity.DaysOfWeek = seance.DaysOfWeek;
            entity.StartTime = seance.StartTime;
            entity.EndTime = seance.EndTime;
            entity.TeacherId = seance.TeacherId;
            entity.Title = seance.Title;
            entity.GroupId = seance.GroupId;
            entity.ModuleId = seance.ModuleId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new Seance()
            {
                Id = Id
            };
            DBContext.Seances.Attach(entity);
            DBContext.Seances.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
