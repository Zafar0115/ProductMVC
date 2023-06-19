using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductMvc.CQRS.CommandQueries;
using ProductMvc.Data;
using ProductMvc.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProductMvc.CQRS.Handlers
{
    public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, bool>
    {
        private readonly AppDbContext dbContext;

        public ProductUpdateHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var p = await dbContext.Products.FindAsync(request.Id);
            if(p is  null) return false;
            Product? updated = new()
            {
                Id= request.Id,
                Name= request.Name,
                Quantity= request.Quantity,
                Price= request.Price,
                Img=request.Img.FileName
            };
            dbContext.Entry<Product>(updated).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
