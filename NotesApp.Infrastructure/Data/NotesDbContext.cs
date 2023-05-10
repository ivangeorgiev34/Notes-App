using Microsoft.EntityFrameworkCore;
using NotesApp.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace NotesApp.Infrastructure.Data
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
        {
        }

        public DbSet<User>? Users { get; set; }

        public DbSet<Note>? Notes { get; set; }

    }
}
