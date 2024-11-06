using Newtonsoft.Json;
using System.Text;

namespace backend.Services
{
    public class FileService
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
                if (i < generatedData.CustomObjects.Count)
                {
                    var customObject = generatedData.CustomObjects[i];
                    if (!string.IsNullOrEmpty(customObject.Name))
                    {
                        entry[customObject.Name] = customObject.Data;
                    }
                }
                if (entry.Count > 0)
                {
                    structuredData.Add(entry);
                }

            }
            string json = JsonConvert.SerializeObject(structuredData, Formatting.Indented);
            return json;
        }
        public string CreateStructuredCsvData(GeneratedData generatedData, int amount)
        {
            var csvBuilder = new StringBuilder();
            var headers = new List<string>();
            if (!string.IsNullOrEmpty(generatedData.Numbers.Name)) headers.Add(generatedData.Numbers.Name);
            if (!string.IsNullOrEmpty(generatedData.OpenStrings.Name)) headers.Add(generatedData.OpenStrings.Name);
            if (!string.IsNullOrEmpty(generatedData.FixedStrings.Name)) headers.Add(generatedData.FixedStrings.Name);
            if (!string.IsNullOrEmpty(generatedData.FirstNames.Name)) headers.Add(generatedData.FirstNames.Name);
            if (!string.IsNullOrEmpty(generatedData.LastNames.Name)) headers.Add(generatedData.LastNames.Name);
            if (!string.IsNullOrEmpty(generatedData.Countries.Name)) headers.Add(generatedData.Countries.Name);
            if (!string.IsNullOrEmpty(generatedData.Ids.Name)) headers.Add(generatedData.Ids.Name);
            csvBuilder.AppendLine(string.Join(",", headers));
            
            for (int i = 0; i < amount; i++)
            {
                var row = new List<string>();

                if (i < generatedData.Numbers.Numbers.Count) row.Add(generatedData.Numbers.Numbers[i].ToString());
                if (i < generatedData.OpenStrings.OpenStrings.Count) row.Add(generatedData.OpenStrings.OpenStrings[i]);
                if (i < generatedData.FixedStrings.FixedStrings.Count) row.Add(generatedData.FixedStrings.FixedStrings[i]);
                if (i < generatedData.FirstNames.Firstnames.Count) row.Add(generatedData.FirstNames.Firstnames[i]);
                if (i < generatedData.LastNames.Lastnames.Count) row.Add(generatedData.LastNames.Lastnames[i]);
                if (i < generatedData.Countries.Countries.Count) row.Add(generatedData.Countries.Countries[i]);
                if (i < generatedData.Ids.Ids.Count) row.Add(generatedData.Ids.Ids[i]);

                csvBuilder.AppendLine(string.Join(",", row));
            }

            return csvBuilder.ToString();
        }
    }
}