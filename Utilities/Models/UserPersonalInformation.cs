using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Utilities.Models;

public class UserPersonalInformation
{
    [Key] public long Id { get; set; }

    public IdentityUser Account { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public DateTime BirthDay { get; set; }

    public AccountType AccountType { get; set; }
}