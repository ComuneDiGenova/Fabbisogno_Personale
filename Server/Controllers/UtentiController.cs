using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FabbPers.Authentication;
using FabbPers.Business;

namespace FabbPers.Controllers
{
    //[Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="98")]
    [ApiController]
    [Route("api/utenti/")]
    public class UtentiController: Microsoft.AspNetCore.Mvc.ControllerBase
    {
    private IUtentiBusiness UtentiBusiness;

    IConfiguration configuration;
        
        public UtentiController(IUtentiBusiness UtentiBusiness, IConfiguration configuration)
        {
            this.UtentiBusiness = UtentiBusiness;
            this.configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<dynamic>> GetUtenti()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return UtentiBusiness.Get();
            
        }

        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpPost]
        public async Task<ActionResult<int>> PostUtenti(Utente  lov)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            int u = UtentiBusiness.Post(lov);
            if (u<0) {
                return BadRequest("Utente già presente");
            } else {
                return u;
            }
            
        }

        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpPut]
        public async Task<ActionResult<int>> PutUtenti(Utente  lov)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            int u = UtentiBusiness.Put(lov);
            if (u<0) {
                return BadRequest();
            } else {
                return u;
            }
            
        }

        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpDelete("{id}")]
        public async Task<int> DeleteUtenti(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return UtentiBusiness.Delete(id);
            
        }
    }
}