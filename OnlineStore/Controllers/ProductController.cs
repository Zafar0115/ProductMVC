using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductMvc.CQRS.CommandQueries;
using ProductMvc.CQRS.Handlers;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new ProductGetAllQuery());
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
