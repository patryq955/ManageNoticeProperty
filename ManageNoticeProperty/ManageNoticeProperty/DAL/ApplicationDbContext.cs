using ManageNoticeProperty.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ManageNoticeProperty.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public DbSet<Album> Album { get; set; }
        public DbSet<Flat> Flat { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<TypeFlat> TypeFlat { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
    }
}