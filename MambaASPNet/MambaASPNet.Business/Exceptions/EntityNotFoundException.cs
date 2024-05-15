using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MambaASPNet.Business.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; set; }
        public EntityNotFoundException(string Name,string? message) : base(message)
        {
            EntityName = Name;
        }
    }
}
