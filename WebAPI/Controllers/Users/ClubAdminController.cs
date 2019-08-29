using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;
using WebAPI.Models.Auth.Roles;

namespace WebAPI.Controllers.Users
{
    [Route("api/ClubAdmin")]
    [ApiController]
    public class ClubAdminController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _singInManager;
        private readonly ApplicationSettings _appSettings;
        private ClubsContext _context;

        public ClubAdminController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<ApplicationSettings> appSettings, ClubsContext context)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;
            _context = context;
        }

        // GET: api/ClubAdmin
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ClubAdmin>>> GetTerrains()
        {
            return await _context.ClubAdmins.Include(ca=>ca.Clubs).ThenInclude(cc=>cc.Terrains).ToListAsync();
        }

        // GET: api/ClubAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubAdmin>> GetTerrains(string id)
        {
            //return await _context.ClubAdmins.Include(ca => ca.Clubs).ThenInclude(cc => cc.Terrains).SingleOrDefaultAsync(c=>c.Id==id);
            return await _context.ClubAdmins.Include(ca => ca.Clubs).SingleOrDefaultAsync(c=>c.Id==id);
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ClubAdmin/Register
        public async Task<Object> PostApplicationUser(RegisterUserModel model)
        {
            var ClubAdmin = new ClubAdmin()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                IsActive = false,
                Gender = model.Gender
            };

            try
            {
                var result = await _userManager.CreateAsync(ClubAdmin, model.Password);
                await _userManager.AddToRoleAsync(ClubAdmin, "ClubAdmin");
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Put : /api/ClubAdmin/Edit
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, ClubAdmin ClubAdmin)
        {
            var user = await _context.ClubAdmins.SingleOrDefaultAsync(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                if (await _userManager.Users.SingleOrDefaultAsync(c => c.UserName == ClubAdmin.UserName) != null)
                {
                    return NoContent();

                }
                user.UserName = ClubAdmin.UserName;
                user.Email = ClubAdmin.Email;
                user.FullName = ClubAdmin.FullName;
                user.PhoneNumber = ClubAdmin.PhoneNumber;
                user.Gender = ClubAdmin.Gender;
                await _userManager.UpdateAsync(user);
                return Ok(user);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}