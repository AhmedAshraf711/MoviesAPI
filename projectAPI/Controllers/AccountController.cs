using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterUserDTO userDTO)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user =new ApplicationUser();
                user.UserName = userDTO.UserName;
                user.Email = userDTO.Email;
               IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    return Ok("Account Add Success");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }

        [HttpPost("adminregister")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminRegistration(RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = userDTO.UserName;
                user.Email = userDTO.Email;
                IdentityResult result = await userManager.CreateAsync(user, userDTO.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    return Ok("Admin Add Success");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userdto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(userdto.UserName);
                if (user != null)
                {
                  bool userfoud= await userManager.CheckPasswordAsync(user,userdto.Password);
                    if (userfoud)
                    {
                        var clamis = new List<Claim>();
                        clamis.Add(new Claim(ClaimTypes.Name, user.UserName));
                        clamis.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        clamis.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var itemRole in roles)
                        {
                            clamis.Add(new Claim(ClaimTypes.Role, itemRole));
                        }

                        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                         SigningCredentials signingCred = 
                                new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken mytoken = new JwtSecurityToken
                            (issuer: config["JWT:VaildIssure"],
                            audience: config["JWT:VaildAduiance"],
                            claims:clamis,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials:signingCred
                            );
                        return Ok(new
                        {
                            tokin = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo
                        });
                    }
                }
                return Unauthorized();
            }
            return BadRequest(ModelState);
        }
    }
}
