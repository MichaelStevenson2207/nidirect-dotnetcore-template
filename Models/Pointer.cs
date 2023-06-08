using Newtonsoft.Json;

namespace nidirect_app_frontend.Models
{
    public sealed class Pointer
    {
        [JsonProperty("Organisation_Name")]
        public string OrganisationName { get; set; }

        [JsonProperty("Sub_Building_Name")]
        public string SubBuildingName { get; set; }

        [JsonProperty("Building_Name")]
        public string BuildingName { get; set; }

        [JsonProperty("Building_Number")]
        public string BuildingNumber { get; set; }

        [JsonProperty("Primary_Thorfare")]
        public string PrimaryThorfare { get; set; }

        [JsonProperty("Alt_Thorfare_Name1")]
        public string AltThorfareName1 { get; set; }

        [JsonProperty("Secondary_Thorfare")]
        public string SecondaryThorfare { get; set; }

        [JsonProperty(nameof(Locality))]
        public string Locality { get; set; }

        [JsonProperty("Townland")]
        public string TownLand { get; set; }

        [JsonProperty(nameof(Town))]
        public string Town { get; set; }

        [JsonProperty(nameof(County))]
        public string County { get; set; }

        [JsonProperty(nameof(Postcode))]
        public string Postcode { get; set; }

        [JsonProperty("BLPU")]
        public string Blpu { get; set; }

        [JsonProperty("Unique_Building_ID")]
        public int UniqueBuildingId { get; set; }

        [JsonProperty("UPRN")]
        public int Uprn { get; set; }

        [JsonProperty("USRN")]
        public int Usrn { get; set; }

        [JsonProperty("Local_Council")]
        public string LocalCouncil { get; set; }

        [JsonProperty("X_COR")]
        public int XCor { get; set; }

        [JsonProperty("Y_COR")]
        public int Ycor { get; set; }

        [JsonProperty("Temp_Coords")]
        public string TempCoords { get; set; }

        [JsonProperty("Building_Status")]
        public string BuildingStatus { get; set; }

        [JsonProperty("Address_Status")]
        public string AddressStatus { get; set; }

        [JsonProperty(nameof(Classification))]
        public string Classification { get; set; }

        [JsonProperty("Creation_Date")]
        public string CreationDate { get; set; }

        [JsonProperty("Commencement_Date")]
        public string CommencementDate { get; set; }

        [JsonProperty("Archived_Date")]
        public string ArchivedDate { get; set; }

        [JsonProperty(nameof(Action))]
        public string Action { get; set; }

        [JsonProperty("UDPRN")]
        public string Udprn { get; set; }

        [JsonProperty("Posttown")]
        public string PostTown { get; set; }
    }
}