using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Utilities.Models;

public class Transaction
{
    [Key] public long Id { get; set; }

    public long Quantity { get; set; }

    public DateTime TransactionDate { get; set; }

    public IdentityUser Buyer { get; set; }

    public Media MediaBought { get; set; }
}