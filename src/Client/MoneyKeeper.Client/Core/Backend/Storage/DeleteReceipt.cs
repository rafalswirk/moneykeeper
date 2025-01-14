using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.Core.Backend.Storage
{
    internal class DeleteReceipt
    {
        public async Task DeleteAsync(int id)
        {
            await Consts.BaseApiUrl.AppendPathSegment($"receipt/storage/{id}").DeleteAsync();
        }
    }
}
