namespace TaskManagementSystem.BL.Services.Abstracts
{
    public interface IAppointmentStatusUpdater
    {
        Task UpdateStatusesAsync(CancellationToken cancellationToken = default);
    }

}
