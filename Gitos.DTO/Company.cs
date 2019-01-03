using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Gitos.DTO
{


    [DataContract]
   public class Company : Base
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string HouseNumber { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public int NumberKBO { get; set; }

        [DataMember]
        public string Language { get; set; }

    }
}
