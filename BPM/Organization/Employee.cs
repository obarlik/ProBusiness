using BPM.Persistency;
using BPM.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Organization
{
    public class Employee : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public User User { get; set; }
        public string Title { get; set; }
        public Department Department { get; set; }
        public Employee ReportsTo { get; set; }
        
        public ICollection<Employee> Assistants { get; }

        public Employee()
        {
        }

        public void AfterConstruction()
        {
        }
    }
}
