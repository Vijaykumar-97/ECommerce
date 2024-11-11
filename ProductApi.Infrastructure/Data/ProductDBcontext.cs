using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Data
{
    public class ProductDBcontext(DbContextOptions<ProductDBcontext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
