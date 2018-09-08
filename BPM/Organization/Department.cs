using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Organization
{
    public class Department : IPersistent
    {        
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }
        public Office Office { get; set; }
        public Employee Manager { get; set; }
        
        public ICollection<Employee> Employees { get; }

        public Department()
        {
        }

        public void AfterConstruction()
        {
        }
    }
}
