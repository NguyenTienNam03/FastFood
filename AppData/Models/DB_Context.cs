using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class DB_Context : DbContext
	{
		public DB_Context() { }
		public DB_Context(DbContextOptions<DB_Context> options) : base(options) { }
		public DbSet<Bill> bills {get;set;}
		public DbSet<BillDetail> billDetails { get; set; }
		public DbSet<Cart> carts { get; set; }
		public DbSet<CartDetail> cartDetails { get; set; }
		public DbSet<MainDishes> mainDishes { get; set; }
		public DbSet<Drinks> drinks { get; set; }
		public DbSet<SideDishes> sideDishes { get; set; }
		public DbSet<ComboFastFood> comboFastFoods { get; set; }
		public DbSet<Voucher> vouchers { get; set; }
		public DbSet<Role> roles { get; set; }
		public DbSet<Customer> customers { get; set; }
		public DbSet<Setkey> setkey { get; set; }
		public DbSet<Payments> payments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source=WINDOWS-10;Initial Catalog=ASSM_Net106;Integrated Security=True");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
