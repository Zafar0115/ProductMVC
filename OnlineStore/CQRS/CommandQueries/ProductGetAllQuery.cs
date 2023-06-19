using MediatR;
using ProductMvc.Models;
using System.Collections.Generic;

namespace ProductMvc.CQRS.CommandQueries
{
    public class ProductGetAllQuery:IRequest<IEnumerable<Product>?>
    {
    }
}
