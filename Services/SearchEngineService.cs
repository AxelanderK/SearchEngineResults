using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using SearchengineResult.Extensions;
using SearchengineResult.Models;
using SearchengineResult.Services;
using SerpApi;
using System.Collections;
using System.Web;

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

        public SearchEngineService(IConfiguration config) {
            _config= config;
        }

        public async Task<List<Result>> Search(string search)
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => ScrapeResultCount("bing", search, "//*[@id=\"b_tween\"]/span[1]")).ContinueWith((f) =>
            {
                if (f.Result != null)
                {
                    Results.Add(f.Result);
                }
            }));
            tasks.Add(Task.Run(() => ScrapeResultCount("yahoo", search, "//*[@id=\"cols\"]/ol/li/div/div/h2/span")).ContinueWith((f) =>
            {
                if (f.Result != null)
                {
                    Results.Add(f.Result);
                }
            }));

            await Task.WhenAll(tasks);

            return Results;
        }

        private async Task<Result> ScrapeResultCount(string searchEngine, string search, string xPath)
        {
            string[] searchWords = search.Split(' ');

            long sum = 0;
            foreach (var searchWord in searchWords)
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseString = await client.GetStringAsync(SearchEngineUrls[searchEngine] + searchWord);
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(responseString);

                    var resultNode = htmlDoc.DocumentNode.SelectSingleNode(xPath);

                    if (resultNode != null)
                    {
                        string resultText = HttpUtility.HtmlDecode(resultNode.InnerText);
                        sum += Int64.Parse(string.Concat(resultText.Where(Char.IsDigit)));
                    }
                }
            }

            return new Result
            {
                Name = searchEngine,
                ResultCount = sum,
            };
        }
    }
}
