using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoloPollster
{
    public class LoginData
    {
        public string username { get; set; }
        public string password { get; set; }
        public LoginData()
        {
            this.username = "default";
            this.password = "default";
        }
    }

    public class LoginDataViewModel
    {
        private LoginData defaultLogin = new LoginData();
        public LoginData Login { get { return this.defaultLogin; } }
    }
}