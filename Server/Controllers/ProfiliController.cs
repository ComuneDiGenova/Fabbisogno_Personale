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
    [Route("api/profili/")]
    public class ProfiliController: Microsoft.AspNetCore.Mvc.ControllerBase
    {
    private IProfiliBusiness profiliBusiness;

    IConfiguration configuration;
        
        public ProfiliController(IProfiliBusiness profiliBusiness, IConfiguration configuration)
        {
            this.profiliBusiness = profiliBusiness;
            this.configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<dynamic>> GetProfili()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return profiliBusiness.Get();
            
        }

        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpPost]
        public async Task<int> PostProfili(Profilo  lov)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return profiliBusiness.Post(lov);
            
        }

        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpPut]
        public async Task<int> PutProfili(Profilo  lov)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return profiliBusiness.Put(lov);
            
        }

        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpDelete("{id}")]
        public async Task<int> DeleteProfili(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return profiliBusiness.Delete(id);
            
        }
    }
}