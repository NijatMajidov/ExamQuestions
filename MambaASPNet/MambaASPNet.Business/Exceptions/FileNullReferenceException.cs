using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MambaASPNet.Business.Exceptions
{
    public class FileNullReferenceException : Exception
    {
        public string PropName { get; set; }
        public FileNullReferenceException(string Name,string? message) : base(message)
        {
            PropName = Name;
        }
    }
}
