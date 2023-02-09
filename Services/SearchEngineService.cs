using Newtonsoft.Json.Linq;
using SearchengineResult.Extensions;
using SearchengineResult.Models;
using SerpApi;
using System.Collections;

namespace SearchengineResult.Services
{
    public class SearchEngineService
    {
        private readonly IConfiguration _config;
        private List<Result?> Results { get; set; } = new List<Result?>();
        private static readonly Dictionary<string, string> SearchQueryStrings = new Dictionary<string, string>() {
            { "google", "q" },
            { "bing", "q" },
            { "yahoo", "p" },
        };
        private string SerpApiKey { get; set; }

        public SearchEngineService(IConfiguration config) {
            _config= config;
            SerpApiKey = _config["SerpApiKey"];
        }

        public IEnumerable<Result?> Search(string search)
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
