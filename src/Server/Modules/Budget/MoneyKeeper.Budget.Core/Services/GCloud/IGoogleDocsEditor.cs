namespace MoneyKeeper.Budget.Core.Services.GCloud
{
    public interface IGoogleDocsEditor
    {
        void AddValueToGoogleDocs(string sheet, string row, string column, string value);
        IEnumerable<string> GetValuesRange(string sheetName, string range);
        Task Init();
    }
}