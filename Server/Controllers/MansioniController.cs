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
    [Authorize]
    [ApiController]
    [Route("api/mansioni/")]
    public class MansioniController: Microsoft.AspNetCore.Mvc.ControllerBase
    {
    private IMansioniBusiness mansioniBusiness;

    IConfiguration configuration;
        
        public MansioniController(IMansioniBusiness mansioniBusiness, IConfiguration configuration)
        {
            this.mansioniBusiness = mansioniBusiness;
            this.configuration = configuration;
        }

        
        [HttpGet]
        public async Task<IEnumerable<dynamic>> GetMansioni()
        {
            string q = Request.Query["q"];
            var identity = User.Identity; //TODO: fare una funzione che estragga dai claim tutti i dati dello user da passare ai controller quando serve

            var token = await HttpContext.GetTokenAsync("access_token");
            return mansioniBusiness.Get(q);
            
        }

        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpPost]
        public async Task<int> PostMansioni(ListOfValue  lov)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return mansioniBusiness.Post(lov);
            
        }

        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpPut]
        public async Task<int> PutMansioni(ListOfValue  lov)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return mansioniBusiness.Put(lov);
            
        }

        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpDelete("{id}")]
        public async Task<int> DeleteMansioni(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return mansioniBusiness.Delete(id);
            
        }
    }
}