using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegisterationApp.Models;

namespace RegisterationApp.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly RegisterationApp.Models.BookContext _context;

        public IndexModel(RegisterationApp.Models.BookContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; }
        public SelectList Genres;
        public string BookGenre { get; set; }

        public async Task OnGetAsync(string bookGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Book
                                            orderby m.Genre
                                            select m.Genre;

            var books = from m in _context.Book
                         select m;

            if (!String.IsNullOrEmpty(bookGenre))
            {
                books = books.Where(x => x.Genre == bookGenre);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Book = await books.ToListAsync();
        }
    }
}
