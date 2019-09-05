using ITI.SkyLord.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel.Account
{
    public class AccountViewModel
    {
        public RegisterViewModel registerViewModel {get;set;}
        public LoginViewModel loginViewModel {get;set;}
        public Dictionary<string, string> Errors { get; set; }

    }
}
