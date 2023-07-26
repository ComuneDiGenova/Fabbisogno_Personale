// SPDX-License-Identifier: EUPL-1.2
// SPDX-License-Identifier: EUPL-1.2
using System.ComponentModel.DataAnnotations;

namespace FabbPers.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username richiesto")]
        public string Username {get;set;}
        [Required(ErrorMessage = "Password richiesta")]
        public string Password {get;set;}
    }
}