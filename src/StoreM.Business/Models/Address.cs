using System;

namespace StoreM.Business.Models
{
    public class Address : Entity
    {
        public Guid ProviderId { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Cep { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        /* EF Relation */
        public Provider Provider { get; set; }
    }
}
