using Newtonsoft.Json.Linq;
using SearchengineResult.Extensions;
using SearchengineResult.Models;
using SerpApi;
using System.Collections;

namespace SearchEngineResult.Services
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

        public SearchEngineService(IConfiguration config) {
            _config= config;
        }

        public IEnumerable<Result?> Search(string search)
        {
            Results.AddIfNotNull(MakeSearchFromEngine("Google", search));
            Results.AddIfNotNull(MakeSearchFromEngine("Bing", search));
            Results.AddIfNotNull(MakeSearchFromEngine("Yahoo", search));

            return Results;
        }

        private Result? MakeSearchFromEngine(string searchEngine, string search)
        {
            string[] searchWords = search.Split(' ');
            string SerpApiKey = _config["SerpApiKey"];

            long sum = 0;
            foreach (var searchWord in searchWords)
            {
                var queryString = SearchQueryStrings[searchEngine.ToLower()];

                Hashtable hs = new Hashtable
                {
                    { queryString, searchWord },
                    { "engine", searchEngine.ToLower() }
                };

                try
                {
                    GoogleSearch serpApiSearch = new GoogleSearch(hs, SerpApiKey);
                    JObject data = serpApiSearch.GetJson();
                    if (data != null) {
                        sum += (long)data.SelectToken("search_information.total_results");
                    }
                }
                catch (SerpApiSearchException) { }
            }

            return new Result
            {
                Name = searchEngine,
                ResultCount = sum,
            };
        }
    }
}
