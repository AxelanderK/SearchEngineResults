using Microsoft.AspNetCore.Mvc.RazorPages;
using SearchengineResult.Models;
using SearchEngineResult.Services;

namespace SearchengineResult.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public readonly SearchEngineService SearchEngineService;
        public List<Result> Results { get; private set; } = new List<Result>();
        public string? Search { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, SearchEngineService searchEngineService)
        {
            _logger = logger;
            SearchEngineService = searchEngineService;
        }

        public void OnGet()
        {
            
        }

        public void OnPost(string Search)
        {
            Results = (List<Result>)SearchEngineService.Search(Search);
        }
    }
}