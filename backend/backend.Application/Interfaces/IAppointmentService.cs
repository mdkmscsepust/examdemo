using backend.Application.Models;

namespace backend.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<(bool, string)> CreateAppointmentAsync(AppointmentInDTO appointmentInDTO);
        Task<(bool, string)> UpdateAppointmentAsync(int id, AppointmentInDTO appointmentInDTO);
        Task<(bool, string)> DeleteAppointmentAsync(int id);
        Task<(IEnumerable<AppointmentOutDTO>, string)> GetAllAppointmentAsync(AppointmentFilterInDTO appointmentFilterInDTO);
        Task<(AppointmentOutDTO, string)> GetByIdAppointmentAsync(int id);
    }
}