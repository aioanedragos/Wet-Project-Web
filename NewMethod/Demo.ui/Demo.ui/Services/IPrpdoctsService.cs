using Demo.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ui.Services
{
    public interface IPrpdoctsService
    {
        Task<IEnumerable<Product>> GetProducts();
    }
}
