using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using Perform_CRUD_Operations.Models;
using Perform_CRUD_Operations.Services;

namespace Perform_CRUD_Operations.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
         private readonly ApplicationDbContext context;

         //pagination functionality
         public int pageIndex = 1;
         public int totalPages = 0;
         private readonly int pageSize = 5;

        //search functionality
        public string search = "";

        public List<Product> Products { get; set; } = new List<Product>();

         public IndexModel(ApplicationDbContext context)
         {
             this.context = context;
         }
         public void OnGet(int? pageIndex, string? search)
         {
             IQueryable<Product> query = context.Products;

             //search functionality
             if(search != null)
             {
                this.search = search;
                query = query.Where(p => p.Name.Contains(search) || p.Brand.Contains(search));
             }

            query = query.OrderByDescending(p => p.Id);

             // pagination functionality
             if (pageIndex == null || pageIndex < 1)
             {
                 pageIndex = 1;
             }

             this.pageIndex = (int) pageIndex;

             decimal count = query.Count();
             totalPages = (int)Math.Ceiling(count / pageSize);
             query = query.Skip((this.pageIndex - 1) * pageSize).Take(pageSize);

             Products = query.ToList();           
         }
       
    }
}
