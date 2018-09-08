using BPM.Locations;
using BPM.Organization;
using BPM.Persistency;
using System;

namespace BPM.Security
{
    public class User : IPersistent, ILocation
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string LoginName { get; set; }
        public string PasswordHash { get; set; }

        public string FullName { get; set; }
        public string IdNumber { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }

        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        public Employee[] Employments { get; set; }

        public User()
        {
        }

        public void AfterConstruction()
        {
        }
    }
}
