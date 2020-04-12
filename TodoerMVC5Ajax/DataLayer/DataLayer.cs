using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoerMVC5Ajax.Models;

namespace TodoerMVC5Ajax.DataLayer
{
    public class DataLayer:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Todo>().ToTable("Todo");
        }

        public DbSet<Todo> Todos { get; set; }
    }
}