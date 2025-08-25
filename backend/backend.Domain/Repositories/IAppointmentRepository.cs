using backend.Domain.Entities;
using backend.Domain.Enums;

namespace backend.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        Task<bool> AddAsync(Appointment appointment);
        Task<bool> UpdateAsync(Appointment appointment);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Appointment>> GetAllAsync(int? patientId,
                int? doctorId,
                VisitType? visitType,
                DateTime? fromDate,
                DateTime? toDate,
                string? diagnosis,
                int page,
                int pageSize);

        Task<Appointment> GetByIdAsync(int id);
    }
}