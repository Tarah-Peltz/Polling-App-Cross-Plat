using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HoloPollster
{
    [DataContract(Name = "Username", Namespace = "holopollster")]
    public class LoginData
    {
        [DataMember()]
        public string username { get; set; }
        [DataMember()]
        public string password { get; set; }
        [DataMember()]
        public int pollsTaken { get; set; }
        [DataMember()]
        public int pollsCreated { get; set; }
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