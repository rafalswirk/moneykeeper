﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services.GCloud
{
    public class SpreadsheetDataEditor
    {
        public string Add(string cellContent, string value)
        {
            if(string.IsNullOrEmpty(cellContent))
            {
                return value;
            }
            if (cellContent.StartsWith("="))
            {
                return cellContent + $"+{value}";
            }
            return $"={cellContent}+{value}";
        }
    }
}
