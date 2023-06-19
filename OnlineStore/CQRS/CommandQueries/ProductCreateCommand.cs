using MediatR;
using Microsoft.AspNetCore.Http;
using ProductMvc.CustomAttributes;
using ProductMvc.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductMvc.CQRS.CommandQueries
{
    public class ProductCreateCommand:IRequest<Product>
    {
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [ValidateImage(5*1024*1024,".jpg",".jpeg",".jfif")]
        public IFormFile Img { get; set; }
    }
}
