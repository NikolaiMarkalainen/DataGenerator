
using System.Text;

namespace backend.Services
{

    public class GenerateRandomService
    {

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
        public Task<string> GenerateRandomString(string charactersLength)
        {
            if (!int.TryParse(charactersLength, out int length) || length <= 0)
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
            return Task.FromResult(result.ToString());
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
    }
}