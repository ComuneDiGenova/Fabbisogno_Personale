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
    [Route("api/motivi/")]
    public class MotiviRichiestaController: Microsoft.AspNetCore.Mvc.ControllerBase
    {
    private IMotiviRichiestaBusiness MotiviRichiestaBusiness;

    IConfiguration configuration;
        
        public MotiviRichiestaController(IMotiviRichiestaBusiness MotiviRichiestaBusiness, IConfiguration configuration)
        {
            this.MotiviRichiestaBusiness = MotiviRichiestaBusiness;
            this.configuration = configuration;
        }

        
        [HttpGet]
        public async Task<IEnumerable<dynamic>> GetMotiviRichiesta()
        {
            string q = Request.Query["q"];
            var identity = User.Identity; //TODO: fare una funzione che estragga dai claim tutti i dati dello user da passare ai controller quando serve

            var token = await HttpContext.GetTokenAsync("access_token");
            return MotiviRichiestaBusiness.Get(q);
            
        }
        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpPost]
        public async Task<int> PostMotiviRichiesta(ListOfValue  lov)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return MotiviRichiestaBusiness.Post(lov);
            
        }
        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpPut]
        public async Task<int> PutMotiviRichiesta(ListOfValue  lov)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return MotiviRichiestaBusiness.Put(lov);
            
        }
        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme, Roles ="1")]
        [HttpDelete("{id}")]
        public async Task<int> DeleteMotiviRichiesta(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return MotiviRichiestaBusiness.Delete(id);
            
        }
    }
}