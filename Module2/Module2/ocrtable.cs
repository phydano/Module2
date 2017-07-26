using Newtonsoft.Json;

namespace Module2
{
    /* Simple class to define the coloumns used in the table*/
    public class ocrtable
    {
        // The id of the row
        [JsonProperty(PropertyName = "Id")]
        public string id { get; set; }

        // The text where we extracted from the image
        [JsonProperty(PropertyName = "Text")]
        public string Ocrtext { get; set; }

    }
}
