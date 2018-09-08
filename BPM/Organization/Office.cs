using System;
using System.Collections.Generic;

namespace BPM.Organization
{
    public class Office : IPersistent, ILocation
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }
        public Company Company { get; set; }
        
        public double Latitude { get; set; }
        public double Longtitude { get; set; }


        public ICollection<Department> Departments { get; }


        public Office()
        {
        }

        public void AfterConstruction()
        {
        }
    }
}