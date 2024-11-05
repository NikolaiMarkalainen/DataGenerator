using backend.JSONConverter;
using backend.Models;
using Newtonsoft.Json.Linq;
// reference https://medium.com/@suman.chatterjee/polymorphic-data-binding-in-net-core-web-api-1423ac8720e4
public class VariableDataConverter : JsonCreationConverter<VariableData>
{
    protected override VariableData Create(Type objectType, JObject jObject)
    {
        if(jObject == null) throw new ArgumentNullException(nameof(jObject));
        if(jObject["min"] != null && jObject["max"] != null)
            return new NumberVariable();
        else if(jObject["fields"] != null)
            return new CustomObject();
        else if(jObject["idType"] != null)
            return new IDObject();
        else if(jObject["fixedString"] != null)
            return new FixedStringObject();
        else if(jObject["key"] != null && jObject["fixed"] != null && jObject["amountFixed"] != null)
            return new CountryString();
        else if(jObject["words"] != null)
            return new OpenString();
        else if(jObject["useProperty"] != null)
            return new UseProp();
        throw new NotImplementedException();
    }
}