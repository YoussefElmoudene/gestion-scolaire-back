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
    public class ModuleController : ControllerBase
    {
        private readonly DBContext DBContext;
        private SpecialiteController specialiteController;

        public ModuleController(DBContext DBContext)
        {
            this.DBContext = DBContext;
            this.specialiteController = new SpecialiteController(DBContext);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Module>>> Get()
        {
            var List = await DBContext.Modules.Select(
                s => new Module
                {
                    Id = s.Id,
                    Name = s.Name,
                    Coef = s.Coef,
                    Specialite = s.Specialite
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
        public async Task<ActionResult<Module>> GetById(int Id)
        {
            Module module = await DBContext.Modules.Select(
                    s => new Module
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Coef = s.Coef,
                        Specialite = s.Specialite
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return module;
            }
        }

        [HttpPost("Add")]
        public async Task<Module> Insert(Module module)
        {
            var entity = new Module()
            {
                Id = module.Id,
                Name = module.Name,
                Coef = module.Coef,
                Specialite = module.Specialite,
                SpecialiteId = module.SpecialiteId
            };
            DBContext.Modules.AddAsync(entity);
            await DBContext.SaveChangesAsync();
            entity.Specialite = specialiteController.GetById(module.SpecialiteId).Result.Value;
            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> Update(Module module)
        {
            var entity = await DBContext.Modules.FirstOrDefaultAsync(s => s.Id == module.Id);
            if(entity == null) return HttpStatusCode.BadRequest;
            entity.Name = module.Name;
            entity.Coef = module.Coef;
            entity.Specialite = module.Specialite;
            entity.SpecialiteId = module.SpecialiteId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new Module()
            {
                Id = Id
            };
            DBContext.Modules.Attach(entity);
            DBContext.Modules.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
