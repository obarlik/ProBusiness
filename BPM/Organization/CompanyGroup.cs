using BPM.Persistency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Organization
{
    public class CompanyGroup : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }
        public Company Lead { get; set; }

        public ICollection<Company> Companies { get; }

        public CompanyGroup()
        {
        }

        public void AfterConstruction()
        {
        }
    }
}
