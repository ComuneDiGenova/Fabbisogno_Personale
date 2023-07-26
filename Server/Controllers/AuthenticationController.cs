using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FabbPers.Business;
using FabbPers.Authentication;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace FabbPers.Controllers
{

    [ApiController]
    [Route("api/login/")]
    public class AuthenticationController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        IConfiguration _configuration;
        private readonly ILogger _logger;
        public AuthenticationController(IConfiguration configuration, ILoggerFactory logFactory)
        {
            _configuration = configuration;
            _logger = logFactory.CreateLogger("Authentication Controller");
        }


        [HttpPost]

        public ActionResult Login([FromBody] LoginModel model)
        {
            _logger.LogInformation("Log message in the About() method");
            try
            {
                UserModel user = new UserModel(model.Username, model.Password);


                if (user != null && user?.Attivo == 1)
                {

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    List<Claim> claims = new List<Claim> {
                    new Claim("Nome",user.Nome),
                    new Claim("Cognome",user.Cognome),
                    new Claim("Matricola", user.Matricola),
                    new Claim("Direzione", user.Direzione),
                    new Claim(ClaimTypes.Role,user.Ruolo.ToString()),
                };

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddYears(1),
                        claims: claims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        role=user.Ruolo.ToString(),
                        direzione=user.Direzione 
                    });


                }
                else
                {
                    return Unauthorized();
                }
            }
            catch
            {
                return Unauthorized();
            }
        }


    }



}