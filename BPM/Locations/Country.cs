using BPM.Persistency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Locations
{
    public class Country : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }
        public string IsoCode2 { get; set; }        
        public string DialCode { get; set; }

        public City Capital { get; set; }

        public ICollection<City> Cities { get; }


        public Country()
        {
        }


        public void AfterConstruction()
        {
        }
    }
}
