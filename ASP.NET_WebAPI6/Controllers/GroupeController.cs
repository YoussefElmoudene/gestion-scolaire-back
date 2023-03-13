using gestion_scolaire.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace gestion_scolaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupeController : ControllerBase
    {
        private readonly DBContext DBContext;
        private SpecialiteController specialiteController;
        public GroupeController(DBContext DBContext)
        {
            this.DBContext = DBContext;
            this.specialiteController = new SpecialiteController(DBContext);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Groupe>>> Get()
        {
            var List = await DBContext.Groupes.Select(
                s => new Groupe
                {
                    Id = s.Id,
                    Name = s.Name,
                    NrStudent = s.NrStudent,
                    SpecialiteId = s.SpecialiteId,
                    Specialite = s.Specialite,
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
        public async Task<ActionResult<Groupe>> GetById(int Id)
        {
            Groupe groupe = await DBContext.Groupes.Select(
                    s => new Groupe
                    {
                        Id = s.Id,
                        Name = s.Name,
                        NrStudent = s.NrStudent,
                        SpecialiteId = s.SpecialiteId,
                        Specialite = s.Specialite,
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (groupe == null)
            {
                return NotFound();
            }
            else
            {
                return groupe;
            }
        }

        [HttpPost("Add")]
        public async Task<Groupe> Insert(Groupe groupe)
        {
            var entity = new Groupe()
            {
                Id = groupe.Id,
                Name = groupe.Name,
                NrStudent = groupe.NrStudent,
                Specialite = groupe.Specialite,
                SpecialiteId= groupe.SpecialiteId,
            };
            DBContext.Groupes.Add(entity);
            await DBContext.SaveChangesAsync();
            entity.Specialite = specialiteController.GetById(groupe.SpecialiteId).Result.Value;
            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> Update(Groupe groupe)
        {
            var entity = await DBContext.Groupes.FirstOrDefaultAsync(s => s.Id == groupe.Id);
            entity.Name = groupe.Name;
            entity.NrStudent = groupe.NrStudent;
            entity.Specialite = groupe.Specialite;
            entity.SpecialiteId = groupe.SpecialiteId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new Groupe()
            {
                Id = Id
            };
            DBContext.Groupes.Attach(entity);
            DBContext.Groupes.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
