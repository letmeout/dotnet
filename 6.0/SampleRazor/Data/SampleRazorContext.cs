#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleRazor.Models;

namespace SampleRazor.Data
{
    public class SampleRazorContext : DbContext
    {
        public SampleRazorContext (DbContextOptions<SampleRazorContext> options)
            : base(options)
        {
        }

        public DbSet<SampleRazor.Models.Movie> Movies { get; set; }
    }
}
