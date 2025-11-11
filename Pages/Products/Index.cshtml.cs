using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        
        public IList<Product> Products { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public async Task OnGetAsync()
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(p => p.Name.ToLower().Contains(SearchTerm.ToLower())); 
            }
            query = SortOrder switch
            {
                "name_desc" => query.OrderByDescending(p => p.Name),
                "price_asc" => query.OrderBy(p => p.Price),
                "category_desc" => query.OrderByDescending(p => p.Category),
                _ => query.OrderBy(p => p.Name),
            };
            Products = await query.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            // var Products=_context.Products.ToList();
        }
    }
}
