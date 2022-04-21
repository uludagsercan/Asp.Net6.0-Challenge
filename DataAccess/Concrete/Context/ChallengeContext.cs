using Entity.Concrete;
using Microsoft.EntityFrameworkCore;



namespace DataAccess.Concrete.Context
{
    public class ChallengeContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-0MA5SJ1\SQLEXPRESS; Database=Challenge; Trusted_Connection=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId).HasName("customer_pk");
            modelBuilder.Entity<Order>().
                HasOne(o => o.Customer).WithMany(o => o.Orders).
                OnDelete(DeleteBehavior.Cascade).
                HasForeignKey(c => c.CustomerId).
                HasConstraintName("order_customer_id_fk");
            modelBuilder.Entity<Order>().Property(o => o.CreateDate).HasDefaultValueSql("getdate()");
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
