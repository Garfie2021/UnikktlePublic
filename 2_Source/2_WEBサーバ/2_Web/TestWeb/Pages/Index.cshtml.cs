using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace TestWeb.Pages
{
    public class IndexModel : PageModel
    {
        public int _CurrentPage { get; set; }

        public static int GetCurrentPage(ISession session)
        {
            var currentPage = session.GetInt32("test");
            if (currentPage == null)
            {
                currentPage = 0;
                session.SetInt32("test", (int)currentPage);
            }

            return (int)currentPage;
        }

        public void OnGet()
        {
            Console.WriteLine("test OnGetAsync() 1 SessionId: " + HttpContext.Session.Id);

            _CurrentPage = GetCurrentPage(HttpContext.Session);
        }

        public IActionResult OnPostNextAsync()
        {
            Console.WriteLine("test OnPostNextAsync() 1 SessionId: " + HttpContext.Session.Id);

            _CurrentPage = GetCurrentPage(HttpContext.Session);
            _CurrentPage += 1;
            HttpContext.Session.SetInt32("test", _CurrentPage);

            return LocalRedirect("/");
        }
    }
}
