using backend.Domain.Entities;
using backend.Domain.Enums;
using backend.Domain.Repositories;
using backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Services
{
    public class AppointmentService : IAppointmentRepository
    {
        private readonly AppDbContext _dbContext;

        public AppointmentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Appointment appointment)
        {
            try
            {
                await _dbContext.AddAsync(appointment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var appointment = await _dbContext.Appointments.FindAsync(id);
                if (appointment is null) return false;
                _dbContext.Remove(appointment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync(int? patientId, int? doctorId, VisitType? visitType, DateTime? fromDate, DateTime? toDate, string? diagnosis, int page, int pageSize)
        {
            try
            {
                var query = _dbContext.Appointments.Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Include(x => x.PrescriptionDetails)
                    .ThenInclude(m => m.Medicine).AsQueryable();

                if (patientId.HasValue)
                    query = query.Where(a => a.PatientId == patientId.Value);

                if (doctorId.HasValue)
                    query = query.Where(a => a.DoctorId == doctorId.Value);

                if (fromDate.HasValue)
                    query = query.Where(a => a.PrescriptionDetails.Any(p => p.StartDate >= fromDate.Value));

                if (toDate.HasValue)
                    query = query.Where(a => a.PrescriptionDetails.Any(p => p.EndDate <= toDate.Value));

                if (visitType.HasValue)
                    query = query.Where(a => a.VisitType == visitType.Value);


                if (!string.IsNullOrEmpty(diagnosis))
                    query = query.Where(a => a.Diagnosis.Contains(diagnosis));

                return await query
                    .OrderBy(a => a.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            try
            {
                var appointment = await _dbContext.Appointments
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Include(x => x.PrescriptionDetails)
                    .ThenInclude(m => m.Medicine)
                .FirstOrDefaultAsync(x => x.Id == id);
                if (appointment is null) return null!;
                return appointment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            try
            {
                _dbContext.Update(appointment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}