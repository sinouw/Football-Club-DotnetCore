using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
namespace WebAPI.Controllers.Users
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //Get : /api/User
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<User>>> getusers() {
            return await _userManager.Users.ToListAsync();
        }

        //Put : /api/User
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        //Put : /api/User/ToggleStatus
        [HttpPut("ToggleStatus/{id}")]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                user.IsActive = !user.IsActive;
                await _userManager.UpdateAsync(user);
                return Ok(user);

            }
            catch
            {
                return NoContent();
            }
        }

        //Delete : /api/User/Delete/id
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(u=>u.Id== id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                await _userManager.DeleteAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}