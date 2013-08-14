using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesModel
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }

        public string Text { get; set; }

        public User User { get; set; }
    }
}
