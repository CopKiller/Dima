using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Models;

public class User : IdentityUser<long>
{
    // RBAC
    public List<IdentityRole<long>>? Roles { get; set; }
}