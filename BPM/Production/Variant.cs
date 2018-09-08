using BPM.Persistency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Production
{
    public class Variant : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public Product Product { get; set; }
        public string ParentValue { get; set; }
        public string Name { get; set; }
        public string ParentCriteria { get; set; }
        public VariantType ValueType { get; set; }
        public string DefaultValue { get; set; }
        public string ValueList{ get; set; }
        
        public ICollection<Variant> SubVariants { get; }
        
        public void AfterConstruction()
        {
        }
    }
}
