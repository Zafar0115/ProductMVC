using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace ProductMvc.CQRS.CommandQueries
{
    public class ProductUpdateCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public IFormFile Img { get; set; }
    }
}
