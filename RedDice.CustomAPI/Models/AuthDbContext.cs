using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedDice.CustomAPI.Models
{
    public class AuthDbContext: IdentityDbContext<IdentityUser>
    {
        public AuthDbContext()
            : base("RedDiceConnection")
        {

        }

        public System.Data.Entity.DbSet<RedDice.CustomAPI.Models.Event> Events { get; set; }
    }
}