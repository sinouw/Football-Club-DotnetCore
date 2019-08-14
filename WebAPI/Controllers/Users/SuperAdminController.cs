using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
    [Route("api/SuperAdmin")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _singInManager;
        private readonly ApplicationSettings _appSettings;
        private ClubsContext _context;

        public SuperAdminController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<ApplicationSettings> appSettings, ClubsContext context)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<SuperAdmin>> getSuperAdmins()
        {
            return await _context.SuperAdmins.ToListAsync();
        }


        [HttpPost]
        [Route("Register")]
        //POST : /api/SuperAdmin/Register
        public async Task<Object> PostAdmin(RegisterUserModel model)
        {
            var superAdmin = new SuperAdmin()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                IsActive = true,
                Gender = model.Gender
            };

            try
            {
                var result = await _userManager.CreateAsync(superAdmin, model.Password);
                await _userManager.AddToRoleAsync(superAdmin,"SuperAdmin");
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Put : /api/SuperAdmin/Edit
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, SuperAdmin superadmin)
        {
            var user = await _context.SuperAdmins.SingleOrDefaultAsync(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                user.UserName = superadmin.UserName;
                user.Email = superadmin.Email;
                user.FullName = superadmin.FullName;
                user.PhoneNumber = superadmin.PhoneNumber;
                user.Gender = superadmin.Gender;
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