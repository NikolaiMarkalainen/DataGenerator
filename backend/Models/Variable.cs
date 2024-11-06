using Newtonsoft.Json;

namespace backend.Models
{
    [JsonConverter(typeof(VariableDataConverter))]
    public class VariableData
    {
        // Blank class binding to all other classs to inherit
    }

    public class CountryString : VariableData
    {
        [JsonProperty("key")]
        public string? Key { get; set; } 

        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("fixed")]
        public bool Fixed { get; set; }

        [JsonProperty("amountFixed")]
        public int AmountFixed { get; set; }
    }

    public class CustomObject : VariableData
    {
        [JsonProperty("fields")]
        public List<Variable> Fields { get; set; }
    }

    public class FixedStringObject : VariableData
    {
        [JsonProperty("fixedString")]
        public string FixedString { get; set; }
    }

    public class IDObject : VariableData
    {
        [JsonProperty("idType")]
        public IdEnum IdType { get; set; }
    }

    public class NumberVariable : VariableData
    {
        [JsonProperty("min")]
        public double Min { get; set; }

        [JsonProperty("max")]
        public double Max { get; set; }

        [JsonProperty("decimalPrecision")]
        public int? DecimalPrecision { get; set; }

        [JsonProperty("decimal")]
        public bool? Decimal { get; set; }
    }

    public class OpenString : VariableData
    {
        [JsonProperty("words")]
        public bool Words { get; set; }

        [JsonProperty("characterLength")]
        public string CharacterLength { get; set; }
    }

    public class UseProp : VariableData
    {
        [JsonProperty("useProperty")]
        public bool UseProperty { get; set; }
    }

    public class Variable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public DataEnum Type { get; set; } 

        [JsonProperty("variableData")]
        public VariableData? VariableData { get; set; }
    }

    public class FileRequest
    {
        [JsonProperty("variables")]
        public List<Variable> Variables {get; set;}
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("jsonFile")]
        public bool JsonFile { get; set; }
    }
}
