using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyntecxhubUserApi.Business.DTOs;
using SyntecxhubUserApi.Data;
using SyntecxhubUserApi.Models;

namespace SyntecxhubUserApi.Controllers
{
    public class UserController : Controller
    {

        private readonly AppDbContext _context;

        public UserController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

       
        [HttpGet("Get-all-users")]
        public IActionResult GetUsers()
        {
            var querry= _context.Users.AsQueryable();

            UserGetAllDTO users = new();

            users.UserList= querry
                .Select(u=>new UserGetOneDTO
                {
                    Name = u.Name,
                    Email = u.Email
                }).ToList();
                   
                    
                
            
            return  Ok(users);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if (id == null)
            {
                return BadRequest("Id not found!");
            }

            var user= _context.Users.Where(p=>p.Id==id)
                .Select(p=> new UserGetOneDTO
                {
                    Name=p.Name,
                    Email=p.Email
                }).First();

            if (user == null)
            {
                return BadRequest("User not found!");
            }


            return Ok(StatusCodes.Status200OK);
        }

        
        [HttpPost("Create-user")]
        public async Task<IActionResult> CreateUser(UsercreatedDTO usercreatedDTO)
        {
            var exsist= await _context.Users.AnyAsync(x=>x.Email==usercreatedDTO.Email);

            if (exsist)
            {
               throw new Exception("Email already exsist!");
            }



            User user = new();

            user.Name = usercreatedDTO.Name;
            user.Email = usercreatedDTO.Email;

             _context.Users.Add(user);
             await _context.SaveChangesAsync();
            

        
            return Ok(StatusCodes.Status201Created);
        }

       
        [HttpPut("{id}")]
        public  IActionResult UpdateUser(int id, UserUpdateDTO userUpdateDTO)
        {
            if (id != userUpdateDTO.Id) return BadRequest();

            var exsist= _context.Users.FirstOrDefault(x=>x.Id==id);

            if(exsist==null) return NotFound();

            exsist.Name= userUpdateDTO.Name;
            exsist.Email= userUpdateDTO.Email;  

            _context.SaveChanges();

            return Ok(StatusCodes.Status204NoContent); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var exsist= _context.Users.FirstOrDefault(x=>x.Id == id);

            if(exsist==null) return NotFound();

            _context.Users.Remove(exsist);
            _context.SaveChanges();
            

            

            return Ok(StatusCodes.Status204NoContent);
        }

    }



    
}
