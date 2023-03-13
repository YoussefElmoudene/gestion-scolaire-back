using gestion_scolaire.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace gestion_scolaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupDetailsController : ControllerBase
    {
        private readonly DBContext DBContext;

        public GroupDetailsController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<GroupDetail>>> Get()
        {
            var List = await DBContext.GroupDetails.Select(
                s => new GroupDetail
                {
                    Id = s.Id,
                    Groupe = s.Groupe,
                    GroupeId = s.GroupeId,
                    Teacher = s.Teacher,
                    TeacherId = s.TeacherId,
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
        public async Task<ActionResult<GroupDetail>> GetById(int Id)
        {
            GroupDetail groupDetail = await DBContext.GroupDetails.Select(
                    s => new GroupDetail
                    {
                        Id = s.Id,
                        Groupe = s.Groupe,
                        GroupeId = s.GroupeId,
                        Teacher = s.Teacher,
                        TeacherId = s.TeacherId,
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (groupDetail == null)
            {
                return NotFound();
            }
            else
            {
                return groupDetail;
            }
        }

        [HttpPost("Add")]
        public async Task<GroupDetail> Insert(GroupDetail groupDetail)
        {
            var entity = new GroupDetail()
            {
                Id = groupDetail.Id,
                TeacherId= groupDetail.TeacherId,
                Groupe = groupDetail.Groupe,
                GroupeId= groupDetail.GroupeId,
                Teacher = groupDetail.Teacher
            };
            DBContext.GroupDetails.Add(entity);
            await DBContext.SaveChangesAsync();

            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> Update(GroupDetail groupDetail)
        {
            var entity = await DBContext.GroupDetails.FirstOrDefaultAsync(s => s.Id == groupDetail.Id);
            entity.Teacher = groupDetail.Teacher;
            entity.Groupe = groupDetail.Groupe;
            entity.GroupeId = groupDetail.GroupeId;
            entity.TeacherId = groupDetail.TeacherId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new GroupDetail()
            {
                Id = Id
            };
            DBContext.GroupDetails.Attach(entity);
            DBContext.GroupDetails.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
