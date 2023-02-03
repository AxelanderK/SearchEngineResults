using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SearchengineResult.Models;
using SerpApi;
using System.Collections;
using System.Web;
using static System.Net.WebRequestMethods;

namespace SearchengineResult.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Result> Results { get; set; } = new List<Result>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public async void OnPost(string search)
        {
            String[] searchWords = search.Split(' ');

            
        }
    }
}