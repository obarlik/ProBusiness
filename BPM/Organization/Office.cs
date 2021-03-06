﻿using BPM.Locations;
using BPM.Persistency;
using System;
using System.Collections.Generic;

namespace BPM.Organization
{
    public class Office : IPersistent, IAddress, ILocation
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Name { get; set; }
        public Company Company { get; set; }

        public Country Country { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

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