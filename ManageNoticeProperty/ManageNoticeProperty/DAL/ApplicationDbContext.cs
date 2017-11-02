﻿using ManageNoticeProperty.Models;
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
        
        DbSet<Album> Album { get; set; }
        DbSet<Flat> Flat { get; set; }
        DbSet<Message> Message { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<TypeFlat> TypeFlat { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}