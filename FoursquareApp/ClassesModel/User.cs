﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesModel
{
    public class User
    {
        public User()
        {
            this.Places = new HashSet<Place>();
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public long Id { get; set; }

        public string Username { get; set; }

        public string AuthCode { get; set; }

        public string SessionKey { get; set; }

        public virtual ICollection<Place> Places { get; set; }
    }
}