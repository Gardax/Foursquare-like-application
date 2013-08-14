using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class CommentModel
    {
        [Key]
        public long Id { get; set; }

        public string Text { get; set; }
    }
}