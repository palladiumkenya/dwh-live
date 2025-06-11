namespace LiveDWAPI.Domain.Gf
{
    public class DimIndicator
    {
        public string? Name { get; set; }
    }

    public class DimRegion
    {
        public string? County { get; set; }
        public string? SubCounty { get; set; }
        public string? Ward { get; set; }
        public string? FacilityName { get; set; }
    }
    
    public class DimSex
    {
        public string? Sex { get; set; }
    }
    
    public class DimAgeGroup
    {
        public string? AgeGroup { get; set; }
    }

    
    public class DimAgency
    {
        public string? Agency { get;  set;}
        public string? PartnerName { get; set; }
    }

    
    public class FactAggregateIndicator
    {
        public string? Indicator { get; set; }
        public int? Numerator { get; set; }
        public int? Denominator { get; set; }
        public string? County { get; set; }
        public string? SubCounty { get; set; }
        public string? Ward { get; set; }
        public string? Sex { get; set; }
        public string? AgeGroup { get; set; }
        public string? FacilityName { get; set; }
        public string? PartnerName { get; set; }
        public string? Agency { get; set; }
        
        public string? MFLCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public decimal? Lat => GetLat();
        public decimal? Long => GetLong();
        public double? Rate => (Numerator * 1.0 / Denominator * 1.0) * 100;

        private decimal? GetLat()
        {
            decimal.TryParse(Latitude, out var lat);
            return lat;
        }
        private decimal? GetLong()
        {
            decimal.TryParse(Longitude, out var lng);
            return lng;
        }
    }
}
