using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wallet_Project.DTOs;
using Wallet_Project.Models;

namespace Wallet_Project.Controllers
{
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Mobile);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userClaims = new List<Claim>
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: userClaims,
                    expires: DateTime.Now.AddHours(8),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized("Invalid login credentials.");
        }

        [HttpPost("register")]
        public async Task<ActionResult<CustomResultDTO>> Register(RegisterUserDTO model)

        {
            CustomResultDTO customResult = new CustomResultDTO();
            if (ModelState.IsValid)
            {
                // dublicated mobile number
                var existingUser = await _userManager.FindByNameAsync(model.Mobile);

                if (existingUser != null)
                {
                    customResult.IsPass = false;
                    customResult.Message = "Mobile number is already registered.";
                    return customResult;
                }

                var user = new User
                {
                    UserName = model.Mobile,
                    Name = model.Name,
                    Mobile = model.Mobile,
                    Role = Data.Enums.Roles.User,
                    Balance = 1000.00m
                };

                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    // add role (Admin/User) to user in identity

                    if (model.Role.ToString() == "Admin")
                    {
                        if (await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        {

                            IdentityResult roleUser = await _roleManager.CreateAsync(new IdentityRole("Admin"));

                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                    }
                    else
                    {
                        if (await _roleManager.RoleExistsAsync("User"))
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                        }
                        else
                        {

                            IdentityResult roleUser = await _roleManager.CreateAsync(new IdentityRole("User"));

                            await _userManager.AddToRoleAsync(user, "User");
                        }
                    }
                    customResult.IsPass = true;
                    customResult.Message = "Account created succesfully.";
                }
                else
                {
                    customResult.IsPass = false;
                    customResult.Message = "Account created failed.";
                    customResult.Data = ModelState;
                }
            }
            else
            {
                customResult.IsPass = false;
                customResult.Message = "Invalid Register Data.";
                customResult.Data = ModelState;
            }
            return customResult;
        }
    }
}

