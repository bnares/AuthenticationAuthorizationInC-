using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_Security.Pages
{
    [Authorize(Policy ="HRManager")]
    public class HRManagerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
