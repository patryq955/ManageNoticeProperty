using ManageNoticeProperty.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.DAL
{
    public class DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbContext()
            : base("DefaultConnection")
        {
        }

        public static DbContext Create()
        {
            return new DbContext();
        }
    }
}