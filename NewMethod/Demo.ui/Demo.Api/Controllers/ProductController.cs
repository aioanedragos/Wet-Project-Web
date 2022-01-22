using Demo.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("getProducts")]
        public IActionResult getProducts()
        {
            List<Product> _productList = new List<Product>();

            Product p = null;

            p = new Product { Id = 1, Name = "Product 1", Brand = "Brand 1", Price = 1, Quantity = 1 };
            _productList.Add(p);
            p = new Product { Id = 2, Name = "Product 2", Brand = "Brand 2", Price = 2, Quantity = 2 };
            _productList.Add(p);
            p = new Product { Id = 3, Name = "Product 3", Brand = "Brand 3", Price = 3, Quantity = 3 };
            _productList.Add(p);
            p = new Product { Id = 4, Name = "Product 4", Brand = "Brand 4", Price = 4, Quantity = 4 };
            _productList.Add(p);
            return Ok(_productList);
        }
    }
}
