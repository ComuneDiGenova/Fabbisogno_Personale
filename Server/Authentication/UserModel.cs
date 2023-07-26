// SPDX-License-Identifier: EUPL-1.2
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using FabbPers.DataAccess;


namespace FabbPers.Authentication
{
  public class UserModel
  {
    public string Nome { get; set; }
    public string Cognome { get; set; }

    public string Matricola { get; set; }

    public int Ruolo { get; set; }
    public int Attivo { get; set; }

    public string Direzione { get; set; }

    public string UserName
    {
      get
      {
        return string.Format("{0} {1} ({2})", this.Cognome, this.Nome, this.Matricola);
      }
    }

    public UserModel()
    {

    }
    public UserModel(string login, string password)
    {
      UtentiDAO ud = new UtentiDAO();
      var x = ud.Check(login, password);
      if (x.Count == 0)
      {
        throw new System.Exception("Utente non trovato");
      }
      else
      {
        var u = x[0];
        this.Matricola = u.MATRICOLA;
        this.Nome = u.NOME;
        this.Cognome = u.COGNOME;
        this.Direzione = u.DIREZIONE;
        this.Ruolo = u.RUOLO;
        this.Attivo = u.ATTIVO;
      }


    }

    public static UserModel FromToken(string token)
    {
      JwtSecurityToken decToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
      var claims = decToken.Claims;
      string nome = claims.Where(x => x.Type == "Nome").FirstOrDefault().Value;
      string cognome = claims.Where(x => x.Type == "Cognome").FirstOrDefault().Value;
      string matricola = claims.Where(x => x.Type == "Matricola").FirstOrDefault().Value;
      string ruolo = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;
      string direzione = claims.Where(x => x.Type == "Direzione").FirstOrDefault().Value;
      int ruoloInt = 0;
      int.TryParse(ruolo, out ruoloInt);
      return new UserModel
      {
        Matricola = matricola,
        Cognome = cognome,
        Nome = nome,
        Direzione = direzione,
        Ruolo = ruoloInt
      };
    }


  }
}