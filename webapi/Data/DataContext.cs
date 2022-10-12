using System;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}

