using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MambaASPNet.Business.Exceptions
{
    public class FileContentTypeException : Exception
    {
        public string PropertyName { get; set; }
        public FileContentTypeException(string propName,string? message) : base(message)
        {
            PropertyName = propName;
        }
    }
}
