using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

             List<object> result = new List<object>();

            foreach (var user in querry.ToList())
            {
                result.Add(new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.CreatedDate
                });


            }


            return  Ok(StatusCodes.Status200OK);
           
        }

        
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if (id == null) return BadRequest();

            var user= _context.Users.Select(x=>new User
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                CreatedDate = x.CreatedDate,
            }).FirstOrDefault(x=>x.Id == id);
           

            return Ok(StatusCodes.Status200OK);
        }

        
        [HttpPost("Create-user")]
        public IActionResult CreateUser(User user)
        {

              if(user == null) return BadRequest();

            User newUser = new User();


            _context.Users.Add(newUser);
            _context.SaveChangesAsync();
        
            //if(newUser.Email != user.Email)
            //{
                
            //else
            //{
            //    return BadRequest("Email already exsist!");
            //}

            return Ok(StatusCodes.Status201Created);
        }

       
        [HttpPut("{id}")]
        public  IActionResult UpdateUser(int id)
        {
          
            var oldUser= _context.Users.FirstOrDefault(x=>x.Id == id);

            User newUser= new User();

            if(oldUser==null) return BadRequest();

            if (newUser != null)
            {
                newUser.Id = oldUser.Id;
                newUser.Email = oldUser.Email;
                newUser.Name = oldUser.Name;
                newUser.CreatedDate = oldUser.CreatedDate; 

            }

            return Ok(StatusCodes.Status204NoContent);
               
            
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var oldUser= _context.Users.First(x=>x.Id == id);

            if (oldUser == null)
                return NotFound();

            _context.Users.Remove(oldUser);
            _context.SaveChangesAsync();

            return Ok(StatusCodes.Status204NoContent);
           

        }

    }



    
}
