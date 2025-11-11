using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceApp.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        public EditModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Product Product { get; set; }    
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product= await _context.Products.FindAsync(id);
            if(Product==null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _context.Products.Update(Product);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
