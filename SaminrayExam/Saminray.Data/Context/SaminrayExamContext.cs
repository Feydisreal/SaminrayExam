using Microsoft.EntityFrameworkCore;
using SaminrayExam.Saminray.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaminrayExam.Saminray.Data.Context
{
    public class SaminrayExamContext : DbContext
    {
        public SaminrayExamContext()
        {
        }

        public SaminrayExamContext(DbContextOptions<SaminrayExamContext> options)
           : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-J5ORU9U;Database=Saminray_db;Trusted_Connection=True; User Id=sa; Password=7; TrustServerCertificate=True;");
        }
    }
}
