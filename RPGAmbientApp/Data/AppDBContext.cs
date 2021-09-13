using Microsoft.EntityFrameworkCore;
using RPGAmbientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGAmbientApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
            
        }

        public DbSet<Emails> Email { get; set; }

        public DbSet<AudioFile> AudioFiles { get; set; }
    }
}
