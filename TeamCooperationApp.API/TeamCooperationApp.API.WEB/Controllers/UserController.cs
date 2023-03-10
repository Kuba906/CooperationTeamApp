using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamCooperationApp.API.WEB.DataDB;
using TeamCooperationApp.API.WEB.Model;

namespace TeamCooperationApp.API.WEB.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        /// User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("User")]

        public async Task<IActionResult> LoginUser([FromBody] UserResponse response)
        {
            teamcooperationdbContext context = new teamcooperationdbContext();
            var data = context.User;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, response.name),
                new Claim(ClaimTypes.Role, "Operator")

            };

            foreach (var item in data)
            {
                if( item.Name == response.name && item.Password == response.password)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UltraSecretKey1235#@#"));
                    var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOpt = new JwtSecurityToken(
                        issuer: "https://localhost:7104",
                        audience: "https://localhost:7104",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: credentials);

                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOpt);

                    return Ok(new {Token = token});
                }
            }

            return Unauthorized();
        }

        [HttpGet]
        [Authorize]
        [Route("CurrentUser")]
        public async Task<IActionResult> CheckUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
            string name = identity.Claims.ElementAt(0).Value.ToString();

            teamcooperationdbContext context = new teamcooperationdbContext();

            return Ok(new {Name = name});

        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserResponse response)
        {
            teamcooperationdbContext context = new teamcooperationdbContext();

            User newUser = new User()
            {
                Name = response.name,
                Password = response.password,
            };

            context.Add(newUser);

            await context.SaveChangesAsync();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UltraSecretKey1235#@#"));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.name)

            };

            var tokenOpt = new JwtSecurityToken(
                issuer: "https://localhost:7104",
                audience: "https://localhost:7104",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOpt);

            return Ok(new { Token = token });


        }


    }
}
