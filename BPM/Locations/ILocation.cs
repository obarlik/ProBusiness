using System.Collections.Generic;
using System.Text;

namespace BPM.Locations
{
    public interface ILocation
    {
        double Latitude { get; set; }
        double Longtitude { get; set; }
    }
}
