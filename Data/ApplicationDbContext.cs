using Microsoft.EntityFrameworkCore;
using System;


namespace NormExample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
