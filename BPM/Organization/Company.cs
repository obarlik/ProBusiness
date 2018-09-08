using BPM.Persistency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Organization
{
    public class Company : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }
        public CompanyGroup Group { get; set; }

        public ICollection<Office> Offices { get; }

        public Company()
        {
        }

        public void AfterConstruction()
        {
        }
    }
}
