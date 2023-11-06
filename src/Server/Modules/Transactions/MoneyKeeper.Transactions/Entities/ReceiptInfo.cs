using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Entities
{
    public class ReceiptInfo
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public bool OcrDataGenerated { get; set; }
        public bool OcrValidationResult { get; set; }
        public bool SpreadsheetEntered { get; set; }
        public DateOnly UploadDate { get; set; }
    }
}
