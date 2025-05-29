using FcmMessaging.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace FcmMessaging.Infrastructure.Persistence.Sql;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Device> Devices => Set<Device>();
   public DbSet<Message> Messages => Set<Message>();
    public DbSet<User> Users => Set<User>();
}
