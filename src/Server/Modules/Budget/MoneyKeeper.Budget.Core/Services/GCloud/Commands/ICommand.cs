using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Services.GCloud.Commands
{
    internal interface ICommand<T>
    {
        T Execute();
    }
}
