using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesModel
{
    public class Place
    {
        public Place()
        {
            this.Comments = new HashSet<Comment>();
            this.Users = new HashSet<User>();
        }

        [Key]
        public long Id { get; set; }

        public int Latitude { get; set; }

        public int Longitude { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
