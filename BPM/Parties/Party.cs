using BPM.Locations;
using BPM.Persistency;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Parties
{
    public abstract class Party : IAddress
    {
        public string Name { get; set; }
        public string TaxNumber { get; set; }

        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }

        public Country Country { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public double Latitude { get; set; }
        public double Longtitude { get; set; }


        public Party()
        {
        }
    }
}
