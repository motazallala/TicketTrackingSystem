using TicketTrackingSystem.Application.Interface;
using TicketTrackingSystem.Application.Model;
using TicketTrackingSystem.Core.Model.Enum;
using TicketTrackingSystem.DAL.Interface;

namespace TicketTrackingSystem.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IUnitOfWork _unitOfWork;
    public DashboardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<int>> GetTotalTicketsAsync()
    {
        try
        {
            var totalTickets = await _unitOfWork.Tickets.CountAsync();
            return Result<int>.Success(totalTickets);
        }
        catch (Exception ex)
        {

            return Result<int>.Failure("There is an error occur!");
        }
    }
    //get all user numbers
    public async Task<Result<int>> GetTotalUsersAsync()
    {

        try
        {
            var totalUser = await _unitOfWork.Users.CountAsync();
            return Result<int>.Success(totalUser);

        }
        catch (Exception ex)
        {

            return Result<int>.Failure("There is an error occur!");
        }
    }
    public async Task<Result<int>> GetTotalMemberTicketAsync(Guid userId)
    {
        try
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user is null)
            {
                return Result<int>.Failure("The user is not exist!");
            }

            if (!user.UserType.Equals(UserType.Member))
            {
                return Result<int>.Failure("The user is not a member!");
            }
            var numberOfTicketForMember = await _unitOfWork.Tickets.CountAsync(c => c.AssignedToId == user.Id);
            return Result<int>.Success(numberOfTicketForMember);
        }
        catch (Exception ex)
        {

            return Result<int>.Failure("There is an error occur!");
        }
    }
    public async Task<Result<int>> GetTotalClientTicketAsync(Guid userId)
    {
        try
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user is null)
            {
                return Result<int>.Failure("The user is not exist!");
            }

            if (!user.UserType.Equals(UserType.Client))
            {
                return Result<int>.Failure("The user is not a client!");
            }
            var numberOfTicketForClient = await _unitOfWork.Tickets.CountAsync(c => c.CreatorId == user.Id);
            return Result<int>.Success(numberOfTicketForClient);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure("There is an error occur!");
        }
    }
}
