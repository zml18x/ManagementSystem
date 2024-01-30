namespace SpaManagementSystem.Core.Models
{
    public class Address
    {
        public Guid Id {get; protected set;}
        public string Country { get; protected set; }
        public string CountryCode { get; protected set; }
        public string Region { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set;}
        public string Street { get; protected set; }
        public string BuildingNumber { get; protected set; }



         public Address(Guid id, string country, string countryCode, string region, string city, string postalCode,
            string street, string buildingNumber)
        {
            Id = id;
            Country = country;
            CountryCode = countryCode;
            Region = region;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNumber = buildingNumber;
        }
    }
}