using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using ProductMvc.CQRS.CommandQueries;
using ProductMvc.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductMvc.CQRS.Handlers
{
    public class ProductCreateHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductCreateHandler(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            Product p = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price,
                Img = Guid.NewGuid()+request.Img.FileName
            };

            string folder = "Products/Images";
            folder = Path.Combine(folder, p.Img);
            string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);

            await request.Img.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            context.Entry<Product>(p).State=EntityState.Added;
            await context.SaveChangesAsync();
            return p;
        }
    }
}
