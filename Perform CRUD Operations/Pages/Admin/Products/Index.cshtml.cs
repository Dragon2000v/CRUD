using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

         public List<Product> Products { get; set; } = new List<Product>();

         public IndexModel(ApplicationDbContext context)
         {
             this.context = context;
         }
         public void OnGet(int? pageIndex)
         {
             IQueryable<Product> query = context.Products;
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
