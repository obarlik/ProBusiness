using BPM.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Organization
{
    public class Employee
    {
        public Employee()
        {
        }
        
        public User User { get; set; }

        public Department Department { get; set; }

        public Employee Manager { get; set; }

        public string Title { get; set; }

        public Role[] Roles { get; set; }
    }
}
