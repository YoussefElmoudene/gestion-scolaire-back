using gestion_scolaire.DTO;
using gestion_scolaire.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace gestion_scolaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialiteController : ControllerBase
    {
        private readonly DBContext DBContext;

        public SpecialiteController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Specialite>>> Get()
        {
            var List = await DBContext.Specialites.Select(
                s => new Specialite
                {
                    Id = s.Id,
                    Name = s.Name
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
        public async Task<ActionResult<Specialite>> GetById(int Id)
        {
            Specialite sp = await DBContext.Specialites.Select(
                    s => new Specialite
                    {
                        Id = s.Id,
                        Name = s.Name
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
        public async Task<Specialite> Insert(Specialite specialite)
        {
            var entity = new Specialite()
            {
                Id = specialite.Id,
                Name = specialite.Name
            };
            DBContext.Specialites.Add(entity);
            await DBContext.SaveChangesAsync();

            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> Update(Specialite specialite)
        {
            var entity = await DBContext.Specialites.FirstOrDefaultAsync(s => s.Id == specialite.Id);
            entity.Name = specialite.Name;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new Specialite()
            {
                Id = Id
            };
            DBContext.Specialites.Attach(entity);
            DBContext.Specialites.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
