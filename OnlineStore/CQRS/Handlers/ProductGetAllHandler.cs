using MediatR;
using OnlineStore.Data;
using ProductMvc.CQRS.CommandQueries;
using ProductMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductMvc.CQRS.Handlers
{
    public class ProductGetAllHandler : IRequestHandler<ProductGetAllQuery, IEnumerable<Product>?>
    {
        private readonly AppDbContext dbContext;

        public ProductGetAllHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>?> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            return dbContext.Products.AsEnumerable();
        }
    }
}
