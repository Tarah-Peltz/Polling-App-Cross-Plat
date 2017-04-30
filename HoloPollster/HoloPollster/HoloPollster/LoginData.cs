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
        public int pollsTaken;
        public int pollsCreated;
        public LoginData()
        {
            this.username = "default";
            this.password = "default";
            pollsCreated = 0;
            pollsTaken = 0;
        }
        public string welcomeMessage
        {
            get
            {
                return $"Welcome { this.username}";
            }
        }
    }

    public class LoginDataViewModel
    {
        private LoginData defaultLogin = new LoginData();
        public LoginData Login { get { return this.defaultLogin; } }
    }
}