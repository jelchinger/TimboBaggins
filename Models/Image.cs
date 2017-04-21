using System;

namespace tester.Models
{
    public class Image
    {

        public int Id { get; set; }

        public Boolean? hasExif { get; set; }

        public string userId { get; set; }

        public string filePath { get; set; }
        
        public char? GPSLatitudeRef { get; set; }

        public string GPSLatitude { get; set; }

        public int? LatitudeDegree { get; set; }

        public int? LatitudeMinute { get; set; }

        public double? LatitudeSecond { get; set; }

        public char? GPSLongitudeRef { get; set; }

        public string GPSLongitude { get; set; }

        public int? LongitudeDegree { get; set; }

        public int? LongitudeMinute { get; set; }

        public double? LongitudeSecond { get; set; }

        public DateTime? photoDate { get; set; }

        public Boolean? isActive { get; set; }

        public DateTime? dateCreated { get; set; }

    }
}