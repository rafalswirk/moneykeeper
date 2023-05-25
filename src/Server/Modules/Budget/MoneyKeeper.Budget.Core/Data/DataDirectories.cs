using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Budget.Core.Data
{
    public class DataDirectories
    {
        private string _receiptImagesPath;
        private string _ocrResultsPath;

        public string ReceiptImagesPath 
        { 
            get => _receiptImagesPath;
            set
            {
                EnsurePathExists(value);
                _receiptImagesPath = value;
            }
        }

        public string OcrResultsPath
        {
            get => _ocrResultsPath;
            set
            {
                EnsurePathExists(value);
                _ocrResultsPath = value;
            }
        }

        private void EnsurePathExists(string path)
        {
            if (Directory.Exists(path))
                return;
            Directory.CreateDirectory(path);
        }
    }
}
