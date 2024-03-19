using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagementAPI.Models.Authentication.Login;
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


        [HttpPost("api/Authentication/Register")]
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
        }

        [HttpPost("api/Authentication/Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            //Check Model State
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check if User Exists
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))  //Check the Password
            {
                //Claim List creation
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //Add role to claimList
                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                //generate token with claims
                var jwtToken = GetToken(authClaims);

                //returning the token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();

        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
