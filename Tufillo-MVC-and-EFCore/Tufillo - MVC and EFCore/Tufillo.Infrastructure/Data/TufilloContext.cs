using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tufillo___MVC_and_EFCore.Models;

namespace Tufillo.Infrastructure.Data
{
    public class TufilloContext : DbContext
    {
        public TufilloContext(DbContextOptions<TufilloContext> options) : base(options)
        {

        }

        //prop       entity     nameyouwanttogiveittotableindb      
        public DbSet<Category> Category { get; set; }
    }
}
