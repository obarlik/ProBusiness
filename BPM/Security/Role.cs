using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Security
{
    public class Role : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        
        public Role()
        {
        }

        public void AfterConstruction()
        {
        }
    }
}
