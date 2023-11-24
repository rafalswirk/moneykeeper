using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Entities
{
    public class ReceiptInfo
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        [Column(TypeName = "boolean")]
        public bool OcrDataGenerated { get; set; }
        [Column(TypeName = "boolean")]
        public bool? OcrValidationResult { get; set; }
        [Column(TypeName = "boolean")]
        public bool SpreadsheetEntered { get; set; }
        public DateOnly UploadDate { get; set; }
    }
}
