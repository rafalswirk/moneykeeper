using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Console.GCloud
{
    internal class SpreadsheetDataEditor
    {
        public string Add(string cellContent, string value)
        {
            if(string.IsNullOrEmpty(cellContent))
            {
                return value;
            }
            if (cellContent.StartsWith("="))
            {
                return cellContent + $"+ {value}";
            }
            return $"={cellContent}+{value}";
        }
    }
}
