using Newtonsoft.Json.Linq;
using SearchengineResult.Extensions;
using SearchengineResult.Models;
using SerpApi;
using System.Collections;

namespace SearchengineResult.Services
{
    public class SearchEngineService : ISearchEngineService
    {
        private readonly IConfiguration _config;
        public List<Result> Results { get; set; } = new List<Result>();
        
        private static readonly Dictionary<string, string> SearchEngineUrls = new Dictionary<string, string>() {
            { "google", "" },
            { "bing", "https://www.bing.com/search?q=hello&search=" },
            { "yahoo", "https://se.search.yahoo.com/search?p=" },
        };
        private string SerpApiKey { get; set; }

        public SearchEngineService(IConfiguration config) {
            _config= config;
            SerpApiKey = _config["SerpApiKey"];
        }

        public async Task<List<Result>> Search(string search)
        {
            Results.AddIfNotNull(MakeSearchFromEngine("google", search));
            Results.AddIfNotNull(MakeSearchFromEngine("bing", search));
            Results.AddIfNotNull(MakeSearchFromEngine("yahoo", search));

            return Results;
        }

        private Result? MakeSearchFromEngine(string searchEngine, string search)
        {
            string[] searchWords = search.Split(' ');

            long sum = 0;
            foreach (var searchWord in searchWords)
            {
                var queryParam = SearchQueryStrings[searchEngine];

                Hashtable hs = new Hashtable
                {
                    { queryParam, searchWord },
                    { "engine", searchEngine }
                };

                try
                {
                    GoogleSearch serpApiSearch = new GoogleSearch(hs, SerpApiKey);
                    JObject data = serpApiSearch.GetJson();
                    if (data != null) {
                        sum += (long)data.SelectToken("search_information.total_results");
                    }
                }
                catch (Exception) { }
            }

            return new Result
            {
                Name = searchEngine,
                ResultCount = sum,
            };
        }
    }
}
