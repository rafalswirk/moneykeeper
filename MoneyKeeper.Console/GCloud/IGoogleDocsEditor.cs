namespace MoneyKeeper.Console.GCloud
{
    public interface IGoogleDocsEditor
    {
        void AddValueToGoogleDocs(string sheet, string row, string column, string value);
        IEnumerable<string> GetValuesRange(string range);
        Task Init();
    }
}