using System;

namespace ProductMvc.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
    }
}
