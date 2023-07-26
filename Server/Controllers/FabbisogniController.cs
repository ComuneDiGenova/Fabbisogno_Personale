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
    [Route("api/fabbisogni/")]
    public class FabbisogniController: Microsoft.AspNetCore.Mvc.ControllerBase
    {
    private IFabbisogniBusiness fabbisogniBusiness;

    IConfiguration configuration;
        
        public FabbisogniController(IFabbisogniBusiness fabbisogniBusiness, IConfiguration configuration)
        {
            this.fabbisogniBusiness = fabbisogniBusiness;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> GetFabbisogni()
        {
            //string q = Request.Query["q"];
            var token = await HttpContext.GetTokenAsync("access_token");
            fabbisogniBusiness.currentUser = UserModel.FromToken(token);
            return fabbisogniBusiness.Get();
            
        }

        [HttpPost]
        public async Task<int> PostFabbisogni(Fabbisogno fabb)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return fabbisogniBusiness.Post(fabb);
            
        }

        [HttpPut]
        [Route("stato/")]
        public async Task<ActionResult<int>> CambiaStato(StateChange sc)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            fabbisogniBusiness.currentUser = UserModel.FromToken(token);
            int res = fabbisogniBusiness.CambiaStato(sc);
            if (res<0)
             return new BadRequestResult();
            else
                return res;
            
        }

        [HttpPut]
        [Route("multiconferma/")]
        public async Task<ActionResult<int>> MultiConferma(MultiStato ids)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            fabbisogniBusiness.currentUser = UserModel.FromToken(token);
            
            int res = fabbisogniBusiness.ConfermaMultipla(ids);
            if (res<0)
             return new BadRequestResult();
            else
                return res;
            
        }

        [HttpPut]
        public async Task<int> PutMansioni(Fabbisogno fabb)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return fabbisogniBusiness.Put(fabb);
            
        }

        [HttpDelete("{id}")]
        public async Task<int> DeleteMansioni(int id)
        {
            var gino = HttpContext.Request;
            var token = await HttpContext.GetTokenAsync("access_token");
            return fabbisogniBusiness.Delete(id);
            
        }
    }
}