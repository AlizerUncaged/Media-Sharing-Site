using Microsoft.EntityFrameworkCore;
using Utilities.Models;

namespace Utilities;

public static class Sql
{
    public static async Task<IEnumerable<Media>> GetMedia(this DbSet<Media> mediaSet, int pageIndex, int pageSize) =>
        await mediaSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

    public static async Task<IEnumerable<Media>> GetAllMedia(this DbSet<Media> mediaSet) =>
        await mediaSet.ToListAsync();

    public static async Task<IEnumerable<Transaction>> GetAllTransactions(this DbSet<Transaction> transactions) =>
        await transactions.Include(x => x.Buyer).Include(x => x.MediaBought).ToListAsync();

    public static async Task<IEnumerable<UserPersonalInformation>> GetAllUserInformation(
        this DbSet<UserPersonalInformation> transactions) =>
        await transactions.Include(x => x.Account).ToListAsync();
}