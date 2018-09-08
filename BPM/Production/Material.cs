using BPM.Persistency;
using System;
using System.Collections.Generic;

namespace BPM.Production
{
    public class Material : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<Supply> Supplies { get; set; }


        public Material()
        {
        }


        public void AfterConstruction()
        {
        }
    }
}