using MediatR;
using OnlineStore.Data;
using ProductMvc.CQRS.CommandQueries;
using ProductMvc.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProductMvc.CQRS.Handlers
{
    public class ProductGetByIdHandler : IRequestHandler<ProductGetByIdQuery, Product?>
    {
        private readonly AppDbContext dbContext;

        public ProductGetByIdHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Product?> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            var p = await dbContext.Products.FindAsync(request.Id);
            return p;
        }
    }
}
