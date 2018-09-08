using BPM.Persistency;
using System;

namespace BPM.Production
{
    public class Requirement : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public Material Material { get; set; }
        public double Amount { get; set; }


        public Requirement()
        {
        }


        public void AfterConstruction()
        {
        }
    }
}