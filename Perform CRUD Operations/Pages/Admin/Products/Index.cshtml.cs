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

        //sort functionality
        public string column = "Id";
        public string orderBy = "desc";

        public List<Product> Products { get; set; } = new List<Product>();

         public IndexModel(ApplicationDbContext context)
         {
             this.context = context;
         }
         public void OnGet(int? pageIndex, string? search, string? column, string? orderBy)
         {
             IQueryable<Product> query = context.Products;

             //search functionality
             if(search != null)
             {
                this.search = search;
                query = query.Where(p => p.Name.Contains(search) || p.Brand.Contains(search));
             }

            //sort functionality
            string[] validColumns = { "Id", "Name", "Brand", "Category", "Price", "CreateAt" };
            string[] validOrderBy = { "desc", "asc" };

            if(!validColumns.Contains(column))
            {
                column = "Id";
            }

            if(!validOrderBy.Contains(orderBy))
            {  
                orderBy = "desc"; 
            }

            this.column = column;
            this.orderBy = orderBy;

            if(column == "Name")
            {
                if(orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Name);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Name);
                }
            }else if (column == "Brand")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Brand);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Brand);
                }
            }else if (column == "Category")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Category);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Category);
                }
            }
            else if (column == "Price")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Price);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Price);
                }
            }
            else if (column == "CreatedAt")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.CreatedAt);
                }
                else
                {
                    query = query.OrderByDescending(p => p.CreatedAt);
                }
            }
            else
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Id);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Id);
                }
            }

            // query = query.OrderByDescending(p => p.Id);

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
