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

namespace WebAPI.Controllers
{
    [Route("api/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _singInManager;
        private readonly ApplicationSettings _appSettings;
        private ClubsContext _context;

        public ClientController(UserManager<User> userManager, SignInManager<User> signInManager,IOptions<ApplicationSettings> appSettings, ClubsContext context)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;
            _context = context;
        }


        //Get : /api/Client
        [HttpGet]
        [EnableQuery()]
        public async Task<ActionResult<IEnumerable<Client>>> getClients()
        {
            return await _context.Clients.Include(c=>c.Reservations).ToListAsync();
        }

        //Get : /api/Client/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> getClient(string id)
        {
            return await _context.Clients.Include(c => c.Reservations).SingleOrDefaultAsync(c=>c.Id==id);
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser(RegisterUserModel model)
        {
            var client = new Client() {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                IsActive = true,
                Gender = model.Gender
            };

            try
            {
                var result = await _userManager.CreateAsync(client, model.Password);
                await _userManager.AddToRoleAsync(client, "Client");
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


       
  
        //Put : /api/Client/Edit
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Client client)
        {
            var user = await _context.Clients.SingleOrDefaultAsync(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
           

                if ( user.UserName != client.UserName ) {
                    return BadRequest();
                }
                user.UserName = client.UserName;
                user.Email = client.Email;
                user.FullName = client.FullName;
                user.PhoneNumber = client.PhoneNumber;
                user.Gender = client.Gender;
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