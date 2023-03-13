using gestion_scolaire.DTO;
using gestion_scolaire.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace gestion_scolaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DBContext DBContext;

        public UserController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<User>>> Get()
        {
            var List = await DBContext.Users.Select(
                s => new User
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Cin = s.Cin,
                    Age = s.Age,
                    Password = s.Password,
                    IsEnabled = s.IsEnabled,
                    Role = s.Role,
                    Created = s.Created,
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


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> login(User user)
        {
            UserDTO User = await DBContext.Users.Select(
                    s => new UserDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Email = s.Email,
                        Cin = s.Cin,
                        Age = s.Age,
                        Password = s.Password,
                        IsEnabled = s.IsEnabled,
                        Role = s.Role,
                        Created = s.Created,
                    })
                .FirstOrDefaultAsync(s => s.Email == user.Email && s.Password == user.Password);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<UserDTO>> GetUserById(int Id)
        {
            UserDTO User = await DBContext.Users.Select(
                    s => new UserDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Email = s.Email,
                        Cin = s.Cin,
                        Age= s.Age,
                        Password = s.Password,
                        IsEnabled = s.IsEnabled,
                        Role = s.Role,
                        Created = s.Created,
                    })
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }
        [HttpGet("GetByEmail")]
        public async Task<User> GetByEmail(string email)
        {
            User User = await DBContext.Users.Select(
                    s => new User
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Email = s.Email,
                        Cin = s.Cin,
                        Age = s.Age,
                        Password = s.Password,
                        IsEnabled = s.IsEnabled,
                        Role = s.Role,
                        Created = s.Created,
                    })
                .FirstOrDefaultAsync(s => s.Email == email);

            if (User == null)
            {
                throw new Exception("User not found.");
            }
            else
            {
                return User;
            }
        }

        [HttpPost("Add")]
        public async Task<User> InsertUser(UserDTO User)
        {
            var entity = new User()
            {
                Id = User.Id,
                Name = User.Name,
                Email = User.Email,
                Cin = User.Cin,
                Age = User.Age,
                Password = User.Password,
                IsEnabled = User.IsEnabled,
                Role = User.Role,
                Created = User.Created
            };
            DBContext.Users.Add(entity);
            await DBContext.SaveChangesAsync();
            // return GetByEmail(entity.Email).Result;
            return entity;
        }

        [HttpPut("Update")]
        public async Task<HttpStatusCode> UpdateUser(User user)
        {
            var entity = await DBContext.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            entity.Name = user.Name;
            entity.Cin = user.Cin;
            entity.Age = user.Age;
            entity.Password = user.Password;
            entity.IsEnabled = user.IsEnabled;
            entity.Created = user.Created;
            entity.Role = user.Role;
            DBContext.Users.Update(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new User()
            {
                Id = Id
            };
            DBContext.Users.Attach(entity);
            DBContext.Users.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
