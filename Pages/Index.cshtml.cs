using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SearchengineResult.Models;
using SearchengineResult.Services;

namespace SearchengineResult.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ISearchEngineService SearchEngineService;
        public List<Result> Results { get; private set; } = new List<Result>();
        public string? Search { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, ISearchEngineService searchEngineService)
        {
            _logger = logger;
            SearchEngineService = searchEngineService;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost(string Search)
        {
            Results = await SearchEngineService.Search(Search);

            return Page();
        }
    }
}