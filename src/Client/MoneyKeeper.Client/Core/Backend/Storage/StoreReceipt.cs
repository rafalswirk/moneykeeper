using Flurl;
using Flurl.Http;
using MoneyKeeper.Client.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.Core.Backend.Storage
{
    internal class StoreReceipt
    {
        public async Task<ReceiptInfoDto> StoreAsync(string fileName, Stream stream)
        {
            var content = new MultipartFormDataContent();
            var response = await Consts.BaseApiUrl.AppendPathSegment("receipt/storage").PostMultipartAsync(
                mp => mp.AddFile("file", stream, Path.GetFileName(fileName)));
            var info = await response.ResponseMessage.Content.ReadAsAsync<ReceiptInfoDto>();
            return info;
        }
    }
}
