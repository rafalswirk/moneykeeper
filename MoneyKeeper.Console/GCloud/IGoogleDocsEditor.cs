namespace MoneyKeeper.Console.GCloud
{
    public interface IGoogleDocsEditor
    {
        Task AddValueToGoogleDocs(string token, string projectId);
        IEnumerable<string> GetValuesRange(string range);
        Task Init();
    }
}