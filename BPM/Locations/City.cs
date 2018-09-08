using BPM.Persistency;
using System;
using System.Collections.Generic;

namespace BPM.Locations
{
    public class City : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<District> Districts { get; }


        public City()
        {
        }


        public void AfterConstruction()
        {
        }
    }
}