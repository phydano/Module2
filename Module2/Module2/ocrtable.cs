using Newtonsoft.Json;

namespace Module2
{
    /* Simple class to define the coloumns used in the table 
     */
    public class ocrtable
    {
        // The id coloumn, where we get or set the id
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        // The Text coloumn, where we get or set the text description
        [JsonProperty(PropertyName = "Text")]
        public string Ocrtext { get; set; }

    }
}
