using BPM.Persistency;
using System;

namespace BPM.Locations
{
    public class District : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }


        public District()
        {
        }


        public void AfterConstruction()
        {
        }
    }
}