using Microsoft.EntityFrameworkCore;
using SimpleWebRtcChat.Web.Entity.Entityes;

namespace SimpleWebRtcChat.Web.Entity
{
    public class DataDbContext: DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options) 
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
