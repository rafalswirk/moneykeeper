namespace MoneyKeeper.Budget.Core.Services.GCloud
{
    public interface IGoogleDocsEditor
    {
        Task AddValueToGoogleDocsAsync(string spreadsheetKey, string sheet, string row, string column, string value);
        Task<IEnumerable<string>> GetValuesRangeAsync(string spreadsheetKey, string sheetName, string range);
    }
}