using Demo.Api.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.ui.Services
{
    public class ProductService : IPrpdoctsService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }
        public async  Task<IEnumerable<Product>> GetProducts()
        {
            return await httpClient.GetJsonAsync<Product[]>("api/product/getProducts");
        }
    }
}
