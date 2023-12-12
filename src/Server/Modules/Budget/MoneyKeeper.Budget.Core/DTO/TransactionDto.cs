﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.DTO
{
    public record TransactionDto(DateTime Date, string Category, double Sum);
}
