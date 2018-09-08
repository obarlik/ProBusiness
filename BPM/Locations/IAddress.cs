using System;
using System.Collections.Generic;
using System.Text;

namespace BPM.Locations
{
    public interface IAddress : ILocation
    {
        Country Country { get; set; }
        City City { get; set; }
        District District { get; set; }
        string ZipCode { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
    }
}
