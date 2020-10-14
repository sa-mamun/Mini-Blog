using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Web.Areas.Admin.Models
{
    public class ModifiedArticle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}