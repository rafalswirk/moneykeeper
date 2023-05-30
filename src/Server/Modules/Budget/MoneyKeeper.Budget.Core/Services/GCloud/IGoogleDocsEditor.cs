namespace MoneyKeeper.Budget.Core.Services.GCloud
{
    public interface IGoogleDocsEditor
    {
        Task AddValueToGoogleDocsAsync(string sheet, string row, string column, string value);
        Task<IEnumerable<string>> GetValuesRangeAsync(string sheetName, string range);
    }
}