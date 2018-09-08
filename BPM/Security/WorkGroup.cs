using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Security
{
    public class WorkGroup
    {
        public WorkGroup()
        {
        }

        public string Name { get; set; }

        public User[] Users { get; set; }

        public Role[] Roles { get; set; }
    }
}
