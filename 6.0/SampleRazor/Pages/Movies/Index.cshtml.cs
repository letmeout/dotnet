#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleRazor.Data;
using SampleRazor.Models;

namespace SampleRazor.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly SampleRazor.Data.SampleRazorContext _context;

        public IndexModel(SampleRazor.Data.SampleRazorContext context)
        {
            _context = context;
        }

        public IList<Movie> Movies { get;set; }

        public async Task OnGetAsync()
        {
            Movies = await _context.Movies.ToListAsync();
        }
    }
}
