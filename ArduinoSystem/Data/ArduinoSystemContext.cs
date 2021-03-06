﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoSystem.Data
{
    public class ArduinoSystemContext : IdentityDbContext<ApplicationUser>
    {
        public ArduinoSystemContext(DbContextOptions<ArduinoSystemContext> options) 
            : base(options)
        {
        }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Entry> Entries { get; set; }
    }
}
