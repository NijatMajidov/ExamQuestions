using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MambaASPNet.Business.Exceptions
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException(string Name,string? message) : base(message)
        {
            PropName = Name;
        }

        public string PropName { get; set; }


    }
}
