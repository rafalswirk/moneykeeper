using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services.GCloud
{
    public class DayToColumnCalculator
    {
        const int HCharacterAsciOffset = 72;
        const int ZCharacterAsciOffset = 90;
        private const int NumberOfCharacters = 26;

        public string CalculateColumn(int day)
        {
            if (day <= 0 || day > 31)
                throw new ArgumentOutOfRangeException(nameof(day), "Day number must be a positive integer lower or equal to 31");

            var asciCode = day + HCharacterAsciOffset;
            if (asciCode > ZCharacterAsciOffset)
                asciCode -= NumberOfCharacters; 

            if (day >= 19)
            {
                return $"A{(char)asciCode}";
            }

            return ((char)asciCode).ToString();
        }
    }
}
