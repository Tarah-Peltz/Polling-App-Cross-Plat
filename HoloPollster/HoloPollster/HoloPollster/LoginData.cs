using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HoloPollster
{
    //Necessary to serialize data for cloud creation
    [DataContract(Name = "Username", Namespace = "holopollster")] 
    public class LoginData
    {
        [DataMember()] //Necessary for serialization
        public string username { get; set; } 
        [DataMember()]
        public string password { get; set; }
        [DataMember()]
        public int pollsTaken { get; set; }
        [DataMember()]
        public int pollsCreated { get; set; }
        public LoginData()
        { //Sets defaults when a LoginData is created
            this.username = "default";
            this.password = "default";
            pollsCreated = 0;
            pollsTaken = 0;
        }

    }

    public class LoginDataViewModel //Creates the viewmodel for binding the username to the welcome message on the homescreen
    {
        private LoginData defaultLogin = new LoginData();
        public LoginData Login { get { return this.defaultLogin; } }
    }
}