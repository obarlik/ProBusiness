using System;

namespace BPM
{
    public class Location
    {
        public Location()
        {
        }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }


        /// <summary>
        /// Calculate distance using the Haversine Formula
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public double DistanceTo(Location location)
        {
            // Coordinates in decimal degrees (e.g. 2.89078, 12.79797)
            var lon1 = Longtitude;
            var lat1 = Latitude;

            var lon2 = location.Longtitude;
            var lat2 = location.Latitude;

            var R = 6371000;  // radius of Earth in meters

            var rad = new Func<double, double>(d => (d * Math.PI) / 180.0);

            var phi_1 = rad(lat1);
            var phi_2 = rad(lat2);

            var delta_phi = rad(lat2 - lat1);
            var delta_lambda = rad(lon2 - lon1);

            var a = Math.Pow(Math.Sin(delta_phi / 2.0), 2.0)
                  + Math.Cos(phi_1) * Math.Cos(phi_2) * Math.Pow(Math.Sin(delta_lambda / 2.0), 2.0);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var meters = R * c;         // output distance in meters
            
            return meters;
        }
    }
}