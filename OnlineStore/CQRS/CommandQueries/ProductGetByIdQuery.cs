using MediatR;
using ProductMvc.Models;
using System;

namespace ProductMvc.CQRS.CommandQueries
{
    public class ProductGetByIdQuery:IRequest<Product?>
    {
        public Guid Id { get; set; }

    }
}
