using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Client.DTO
{
    public record ReceiptInfoDto(int Id, string ImageName, bool OcrDataGenerated, bool OcrDataValidationResult, bool SpreadsheetEntered, DateTime UploadDate);
}
