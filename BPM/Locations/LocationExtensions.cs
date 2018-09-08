using System;

namespace BPM.Locations
{
    public static class LocationExtensions
    {
        /// <summary>
        /// Calculate distance using the Haversine Formula
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static double DistanceTo(this ILocation from, ILocation to)
        {
            // Coordinates in decimal degrees (e.g. 2.89078, 12.79797)
            var lon1 = from.Longtitude;
            var lat1 = from.Latitude;

            var lon2 = to.Longtitude;
            var lat2 = to.Latitude;

            var R = 6371000.0;  // radius of Earth in meters

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
