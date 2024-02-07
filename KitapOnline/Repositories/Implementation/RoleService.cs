using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public interface IRoleService
{
    Task<bool> CreateRoleAsync(string roleName);
    Task<bool> DeleteRoleAsync(string roleName);
    Task<bool> IsRoleExists(string roleName);
}

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<bool> CreateRoleAsync(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            return result.Succeeded;
        }

        return false;
    }

    public async Task<bool> DeleteRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role != null)
        {
            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        return false;
    }

    public async Task<bool> IsRoleExists(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        return role != null;
    }

}
