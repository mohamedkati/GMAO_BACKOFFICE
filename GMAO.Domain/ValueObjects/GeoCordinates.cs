
using GMAO.Domain.Common;

namespace GMAO.Domain.ValueObjects
{
    public class GeoCoordinates : ValueObject
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public GeoCoordinates(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentException("Latitude must be between -90 and 90");
            if (longitude < -180 || longitude > 180)
                throw new ArgumentException("Longitude must be between -180 and 180");

            Latitude = latitude;
            Longitude = longitude;
        }

        public double DistanceTo(GeoCoordinates other)
        {
            // Haversine formula for distance calculation
            const double earthRadius = 6371; // km

            var dLat = ToRadians(other.Latitude - Latitude);
            var dLon = ToRadians(other.Longitude - Longitude);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(Latitude)) * Math.Cos(ToRadians(other.Latitude)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadius * c;
        }

        private static double ToRadians(double degrees) => degrees * Math.PI / 180;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
