using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebClient.Models
{
    [DataContract]
    public class UserLoggedModel
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }
    }
}