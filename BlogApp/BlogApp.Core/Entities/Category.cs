using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Entities
{
    public class Category : BaseEntity
    {
        public virtual String Name { get; set; }
        public virtual IList<Article> Articles { get; set; }

        public Category()
        {
            Articles = new List<Article>();
        }
    }
}
