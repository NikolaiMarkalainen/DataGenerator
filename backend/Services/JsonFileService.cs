using Newtonsoft.Json;

namespace backend.Services
{
    public class JsonFileService
    {
        
    public string CreateStructuredJsonData(GeneratedData generatedData, int amount)
    {
        var structuredData = new List<Dictionary<string, object>>();

        // formatted json file with object filled
        for (int i = 0; i < amount; i++)
        {
            var entry = new Dictionary<string, object>();

            if (i < generatedData.Numbers.Numbers.Count)
            {
                entry[generatedData.Numbers.Name] = generatedData.Numbers.Numbers[i];
            }

            if (i < generatedData.OpenStrings.OpenStrings.Count)
            {
                entry[generatedData.OpenStrings.Name] = generatedData.OpenStrings.OpenStrings[i];
            }

            if (i < generatedData.FixedStrings.FixedStrings.Count)
            {
                entry[generatedData.FixedStrings.Name] = generatedData.FixedStrings.FixedStrings[i];
            }

            if (i < generatedData.FirstNames.Firstnames.Count)
            {
                entry[generatedData.FirstNames.Name] = generatedData.FirstNames.Firstnames[i];
            }

            if (i < generatedData.LastNames.Lastnames.Count)
            {
                entry[generatedData.LastNames.Name] = generatedData.LastNames.Lastnames[i];
            }

            if (i < generatedData.Countries.Countries.Count)
            {
                entry[generatedData.Countries.Name] = generatedData.Countries.Countries[i];
            }

            if (i < generatedData.Ids.Ids.Count)
            {
                entry[generatedData.Ids.Name] = generatedData.Ids.Ids[i];
            }

            if (entry.Count > 0)
            {
                structuredData.Add(entry);
            }
        }
        string json = JsonConvert.SerializeObject(structuredData, Formatting.Indented);
        return json;
    }
    }
}