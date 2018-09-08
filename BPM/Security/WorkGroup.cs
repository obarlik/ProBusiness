using BPM.Persistency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Security
{
    public class WorkGroup : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }

        public User[] Users { get; set; }
        public Role[] Roles { get; set; }
        
        public WorkGroup()
        {
        }

        public void AfterConstruction()
        {
        }
    }
}
