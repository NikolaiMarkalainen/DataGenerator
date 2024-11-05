
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
        private readonly JsonFileService _jsonFileService;

        public GenerateRandomService(JsonFileService jsonFileService, WordsService wordsService, FirstnamesService firstnamesService, SurnamesService surnamesService, CountriesService countriesService)
        {
            _jsonFileService = jsonFileService;
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
                        var countries = await _countriesService.GenerateCountryDataAsync(countryString, fileRequest.Amount);
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

                        // case DataEnum.RANDOM_CUSTOM_OBJECT:
                        //     break;

                        case DataEnum.RANDOM_ID:
                            if(variable.VariableData is IDObject idObject)
                            {
                               string id = await GenerateRandomId(idObject.IdType);
                               result.Ids.Name = variable.Name;
                               result.Ids.Ids.Add(id);
                            }
                            break;
                    }
                };
            }
            var json = _jsonFileService.CreateStructuredJsonData(result, fileRequest.Amount);

            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "generated"); 
            Directory.CreateDirectory(directoryPath);
            string fileName = $"GeneratedData_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.json";
            string filePath = Path.Combine(directoryPath, fileName);
            await File.WriteAllTextAsync(filePath, json);

            return filePath;
        }
    }
}