namespace TicketTrackingSystem.Application.Interface;
public interface IPermissionService
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionName);
    Task<Dictionary<string, bool>> HasPermissionAsync(Guid userId, params string[] permissionNames);
}
