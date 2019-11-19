using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XC.WebAPI.Context;
using XC.WebAPI.Models;

namespace XC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly XCContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UserController(XCContext context)
        {
            _context = context;
            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new Sys_User { Name = "Admin" });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sys_User>>> GetUserItems()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sys_User>> GetUserItem(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpPost]
        public async Task<ActionResult<Sys_User>> PostUserItem(Sys_User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserItem), new { id = user.Id }, user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(string id, Sys_User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.Users.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _context.Users.Remove(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}