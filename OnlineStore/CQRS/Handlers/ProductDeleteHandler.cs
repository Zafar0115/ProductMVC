using MediatR;
using ProductMvc.CQRS.CommandQueries;
using ProductMvc.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ProductMvc.CQRS.Handlers
{
    public class ProductDeleteHandler : IRequestHandler<ProductDeleteCommand, bool>
    {
        private readonly AppDbContext dbContext;

        public ProductDeleteHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var p = await dbContext.Products.FindAsync(request.Id);
            if (p == null) return false;
            dbContext.Products.Remove(p);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
