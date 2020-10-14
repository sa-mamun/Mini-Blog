using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Exceptions
{
    public class DuplicationException : Exception
    {
        public DuplicationException(string message)
            : base(message)
        {

        }
    }
}
