using System;
using System.Collections.Generic;
using BPM.Parties;
using BPM.Persistency;

namespace BPM.Production
{
    public class Supplier : Party, IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }


        public ICollection<Supply> Supplies { get; }


        public Supplier()
        {
        }


        public void AfterConstruction()
        {
        }
    }
}