using BPM.Organization;
using System;

namespace BPM.Security
{
    public class User
    {
        public User()
        {
        }

        public string Name { get; set; }

        public Location Location { get; set; }
        
        public Employee[] Positions { get; set; }

        public Role[] Roles { get; set; }
    }
}
