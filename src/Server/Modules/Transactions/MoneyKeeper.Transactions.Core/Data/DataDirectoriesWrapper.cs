using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Transactions.Core.Data
{
    public class DataDirectoriesWrapper
    {
        private readonly IDirectory _directory;
        private readonly DataDirectories _dataDirectories;

        public string ReceiptImagesPath
        {
            get
            {
                EnsurePathExists(_dataDirectories.ReceiptImagesPath);
                return _dataDirectories.ReceiptImagesPath;
            }
        }

        public string OcrResultsPath
        {
            get
            {
                EnsurePathExists(_dataDirectories.OcrResultsPath);
                return _dataDirectories.OcrResultsPath;
            }
        }

        public DataDirectoriesWrapper(IDirectory directory, DataDirectories dataDirectories)
        {
            _directory = directory;
            _dataDirectories = dataDirectories;
        }

        private void EnsurePathExists(string path)
        {
            if (_directory.Exists(path))
                return;
            _directory.CreateDirectory(path);
        }
    }
}
