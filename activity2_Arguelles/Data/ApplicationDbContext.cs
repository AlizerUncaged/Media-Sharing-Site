using Media_Sharing_Site.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Utilities.Models;

namespace Media_Sharing_Site.Data;

public class ApplicationDbContext : IdentityDbContext
{
    /// <summary>
    /// Where user information are stored.
    /// </summary>
    public DbSet<UserPersonalInformation> UserPersonalInformations { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }
    
    public DbSet<Media> Medias { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}