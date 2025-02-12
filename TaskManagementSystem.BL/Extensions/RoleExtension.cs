using TaskManagementSystem.CORE.Enums;

namespace TaskManagementSystem.BL.Extensions
{
    public static class RoleExtension
    {
        public static string AddRole(this Roles role)
        {
            return role switch
            {
                Roles.User => nameof(Roles.User),
                Roles.Admin => nameof(Roles.Admin),
            };
        }
    }
}
