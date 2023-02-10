using SearchengineResult.Models;

namespace SearchengineResult.Services
{
    public interface ISearchEngineService
    {
        List<Result> Results { get; set; }
        static readonly Dictionary<string, string>? SearchEngineUrls;

        Task<List<Result>> Search(string search);
    }
}
