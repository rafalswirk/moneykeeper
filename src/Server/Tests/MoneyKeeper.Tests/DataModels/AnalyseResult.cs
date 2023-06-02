using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.UnitTests.DataModels
{
    internal record AnalyseResult(string NIP, DateTime Date, double Total);
}
