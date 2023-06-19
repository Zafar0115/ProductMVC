using MediatR;
using System;

namespace ProductMvc.CQRS.CommandQueries
{
    public class ProductDeleteCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
