
using System.Text;
using backend.Models;

namespace backend.Services
{

    public class GenerateRandomService
    {
        private readonly WordsService _wordsService;
        private readonly FirstnamesService _firstNamesService;

        private readonly SurnamesService _surnamesService;

        private readonly CountriesService _countriesService;
        private readonly FileService _fileService;

        public GenerateRandomService(FileService fileService, WordsService wordsService, FirstnamesService firstnamesService, SurnamesService surnamesService, CountriesService countriesService)
        {
            _fileService = fileService;
            _wordsService = wordsService;
            _firstNamesService = firstnamesService;
            _surnamesService = surnamesService;
            _countriesService = countriesService;
        }

        public Task<double> GenerateRandomNumber(NumberVariable numberVariable)
        {
            var random = new Random();
            double randomNumber;
            if(numberVariable.Decimal == true)
            {
                if(numberVariable.DecimalPrecision.HasValue && numberVariable.DecimalPrecision > 0)
                {
                    double range = numberVariable.Max - numberVariable.Min;
                    randomNumber = Math.Round(numberVariable.Min + random.NextDouble() * range, numberVariable.DecimalPrecision.Value);
                }
                else
                {
                    randomNumber = random.Next((int)numberVariable.Min, (int)numberVariable.Max + 1);
                }
            }
            else 
            {
                randomNumber = random.Next((int)numberVariable.Min, (int)numberVariable.Max + 1);
            }

            return Task.FromResult(randomNumber);
        }
        public async Task<string> GenerateRandomString(OpenString openString)
        {
            if(openString.Words)
            {
                string word = await _wordsService.GetRandomWordAsync();
                return word;
            }
            if (!int.TryParse(openString.CharacterLength, out int length) || length <= 0)
            {
                throw new ArgumentException("charactersLength must be a positive integer.");
            }
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            var result = new StringBuilder();

            for(int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }
        public Task<string> GenerateRandomId(IdEnum idType)
        {
            if(idType == IdEnum.NUMBER)
            {
                var random = new Random();
                int randomIdNumber = random.Next(1, 100000 + 1);
                return Task.FromResult(randomIdNumber.ToString());
            }
            if(idType == IdEnum.UUID)
            {
                return Task.FromResult(Guid.NewGuid().ToString());
            }
            throw new ArgumentException("Wrong value for idType");
        }
        public async Task<string> GenerateObjectToData(FileRequest fileRequest)
        {
            var result = new GeneratedData();

            // handle multiconditional variables in own loop;
            foreach (var variable in fileRequest.Variables)
            {
                if (variable.Type == DataEnum.RANDOM_COUNTRY && variable.VariableData is CountryString countryString)
                {
                    if (countryString.AmountFixed > 0)
                    {
                         List<string> countries = await _countriesService.GenerateCountryDataAsync(countryString, fileRequest.Amount) as List<string>;
                        result.Countries.Name = variable.Name;
                        result.Countries.Countries = countries;
                    }
                }
            }

            for(int i = 0; i < fileRequest.Amount; i++){
            foreach (var variable in fileRequest.Variables)
                {
                    switch (variable.Type)
                    {
                        case DataEnum.NUMBER:
                            if(variable.VariableData is NumberVariable numberVariable)
                            {
                                double number = await GenerateRandomNumber(numberVariable);
                                
                                result.Numbers.Name = variable.Name;
                                result.Numbers.Numbers.Add(number);
                            }
                            break;

                        case DataEnum.OPEN_STRING:
                            if(variable.VariableData is OpenString openString)
                            {
                                string text = await GenerateRandomString(openString);

                                result.OpenStrings.Name = variable.Name;
                                result.OpenStrings.OpenStrings.Add(text);
                            }
                            
                            break;

                        case DataEnum.FIXED_STRING:
                            if(variable.VariableData is FixedStringObject fixedString)
                            {
                                result.FixedStrings.Name = variable.Name;
                                result.FixedStrings.FixedStrings.Add(fixedString.FixedString);
                            }
                            break;

                        case DataEnum.RANDOM_FIRST_NAME:
                            if(variable.VariableData is UseProp useProp)
                            {
                                string name = await _firstNamesService.GetRandomFirstnameAsync();
                                result.FirstNames.Name = variable.Name;
                                result.FirstNames.Firstnames.Add(name); 
                            }
                            break;

                        case DataEnum.RANDOM_LAST_NAME:
                            if(variable.VariableData is UseProp useProp1)
                            {
                                string surname = await _surnamesService.GetRandomSurnameAsync();
                                result.LastNames.Name = variable.Name;
                                result.LastNames.Lastnames.Add(surname);
                            }
                            break;

                        case DataEnum.RANDOM_CUSTOM_OBJECT:
                            if(variable.VariableData is CustomObject customObject)
                            {
                                var customData = await GenerateCustomObjectData(customObject);
                                result.CustomObjects.Add(new GeneratedObjects
                                {
                                    Name = variable.Name,
                                    Data = customData
                                });
                            }
                             break;

                        case DataEnum.RANDOM_ID:
                            if(variable.VariableData is IDObject idObject)
                            {
                               string id = await GenerateRandomId(idObject.IdType);
                               result.Ids.Name = variable.Name;
                               result.Ids.Ids.Add(id);
                            }
                            break;
                        case DataEnum.RANDOM_COUNTRY:
                        break;

                        default:
                        throw new NotSupportedException($"Unhandled type: {variable.Type}");
                    }
                };
            }
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "generated");
            Directory.CreateDirectory(directoryPath);

            string filePath;
            if(fileRequest.JsonFile)
            {
                var json = _fileService.CreateStructuredJsonData(result, fileRequest.Amount);
                string fileName = $"GeneratedData_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.json";
                filePath = Path.Combine(directoryPath, fileName);
                await File.WriteAllTextAsync(filePath, json);
            }
            else {
                var csv = _fileService.CreateStructuredCsvData(result, fileRequest.Amount);
                string fileName = $"GeneratedData_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                filePath = Path.Combine(directoryPath, fileName);
                await File.WriteAllTextAsync(filePath, csv);            
            }

            return filePath;
        }
        private async Task<Dictionary<string, object>> GenerateCustomObjectData(CustomObject customObject)
        {
            var customData = new Dictionary<string, object>();
            foreach (var variable in customObject.Fields)
            {
                if (variable.Type == DataEnum.RANDOM_COUNTRY && variable.VariableData is CountryString countryString)
                {
                    if (countryString.AmountFixed > 0)
                    {
                        var countries = await _countriesService.GenerateCountryDataAsync(countryString, 1);
                        customData[variable.Name] = countries;
                    }
                }
            }
            foreach (var field in customObject.Fields)
            {
                switch (field.Type)
                {
                    case DataEnum.NUMBER:
                        if (field.VariableData is NumberVariable numberVariable)
                        {
                            double number = await GenerateRandomNumber(numberVariable);
                            customData[field.Name] = number;
                        }
                        break;

                    case DataEnum.OPEN_STRING:
                        if (field.VariableData is OpenString openString)
                        {
                            string text = await GenerateRandomString(openString);
                            customData[field.Name] = text;
                        }
                        break;

                    case DataEnum.FIXED_STRING:
                        if (field.VariableData is FixedStringObject fixedString)
                        {
                            customData[field.Name] = fixedString.FixedString;
                        }
                        break;

                    case DataEnum.RANDOM_FIRST_NAME:
                        if (field.VariableData is UseProp)
                        {
                            string name = await _firstNamesService.GetRandomFirstnameAsync();
                            customData[field.Name] = name;
                        }
                        break;

                    case DataEnum.RANDOM_LAST_NAME:
                        if (field.VariableData is UseProp)
                        {
                            string surname = await _surnamesService.GetRandomSurnameAsync();
                            customData[field.Name] = surname;
                        }
                        break;

                    case DataEnum.RANDOM_ID:
                        if (field.VariableData is IDObject idObject)
                        {
                            string id = await GenerateRandomId(idObject.IdType);
                            customData[field.Name] = id;
                        }
                        break;

                    case DataEnum.RANDOM_CUSTOM_OBJECT:
                        if (field.VariableData is CustomObject nestedCustomObject)
                        {
                            customData[field.Name] = await GenerateCustomObjectData(nestedCustomObject);
                        }
                        break;
                    case DataEnum.RANDOM_COUNTRY:
                    break;

                    default:
                        throw new NotSupportedException($"Unhandled type: {field.Type}");
                }
            }

            return customData;
        }   
    }
}