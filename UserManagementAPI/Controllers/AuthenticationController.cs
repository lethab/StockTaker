using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models.Authentication.SignUp;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {
            //Check Model State
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check if User Exists
            var userExists = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExists != null)
            {
                //return StatusCode(StatusCodes.Status403Forbidden,
                //    new Response { Status = "Error", ReasonPhrase = "User Exists" });

                return StatusCode(StatusCodes.Status403Forbidden);
            }

            //Add User in the Database
            IdentityUser user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username
            };


            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, registerUser.Password);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);//return StatusCode(StatusCodes.Status500InternalServerError,
                                                                                //    new Response { Status = "Error", ReasonPhrase = "User Failed to Create" });
                }

                //Add Role to user
                await _userManager.AddToRoleAsync(user, role);
                return StatusCode(StatusCodes.Status201Created);//return StatusCode(StatusCodes.Status201Created,
                                                              //    new Response { Status = "Error", ReasonPhrase = "User Created" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);//return StatusCode(StatusCodes.Status500InternalServerError,
                                                                            //    new Response { Status = "Error", ReasonPhrase = "This role does not exist" });
            }



            //return result.Succeeded
            //    ? StatusCode(StatusCodes.Status201Created)//return StatusCode(StatusCodes.Status201Created,
            //                                              //    new Response { Status = "Error", ReasonPhrase = "User Created" });               
            //    : StatusCode(StatusCodes.Status500InternalServerError);//return StatusCode(StatusCodes.Status500InternalServerError,
            //                                                           //    new Response { Status = "Error", ReasonPhrase = "User Failed to Create" });


            //Assign Role
        }
    }
}
