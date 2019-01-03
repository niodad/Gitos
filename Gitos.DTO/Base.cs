using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Gitos.DTO
{
    [DataContract]
    public abstract class Base
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
