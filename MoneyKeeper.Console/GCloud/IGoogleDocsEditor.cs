namespace MoneyKeeper.Console.GCloud
{
    public interface IGoogleDocsEditor
    {
        Task AddValueToGoogleDocs();
        IEnumerable<string> GetValuesRange(string range);
        Task Init();
    }
}