using backend.Application.Interfaces;
using backend.Application.Models;
using backend.Domain.Entities;
using backend.Domain.Repositories;

namespace backend.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<(bool, string)> CreateAppointmentAsync(AppointmentInDTO appointmentInDTO)
        {
            var response = await _appointmentRepository.AddAsync(new Appointment
{
    PatientId = appointmentInDTO.PatientId,
    DoctorId = appointmentInDTO.DoctorId,
    AppointmentDate = appointmentInDTO.AppointmentDate,
    Diagnosis = appointmentInDTO.Diagnosis,
    Notes = appointmentInDTO.Notes ?? string.Empty,
    VisitType = appointmentInDTO.VisitType,
    PrescriptionDetails = appointmentInDTO.PrescriptionDetails
        .Select(p => new PrescriptionDetail
        {
            MedicineId = p.MedicineId,
            Dosage = p.Dosage ?? "2x",
            Notes = p.Notes ?? string.Empty,
            StartDate = p.StartDate,
            EndDate = p.EndDate
        })
        .ToList()
            });

            return (response, "save success");
        }

        public async Task<(bool, string)> DeleteAppointmentAsync(int id)
        {
            var response = await _appointmentRepository.DeleteAsync(id);
            if (!response)
                return (response, "deleted failed.");

            return (response, "deleted success.");
        }

        public async Task<(IEnumerable<AppointmentOutDTO>, string)> GetAllAppointmentAsync(AppointmentFilterInDTO appointmentFilterInDTO)
        {
            var appointments = await _appointmentRepository
            .GetAllAsync(appointmentFilterInDTO.PatientId,
                appointmentFilterInDTO.DoctorId,
                appointmentFilterInDTO.VisitType,
                appointmentFilterInDTO.FromDate,
                appointmentFilterInDTO.ToDate,
                appointmentFilterInDTO.Diagnosis,
                appointmentFilterInDTO.Page,
                appointmentFilterInDTO.PageSize);

            var response = appointments.Select(x => new AppointmentOutDTO
                            {
                                Id = x.Id,
                                PatientId = x.PatientId,
                                DoctorId = x.DoctorId,
                                AppointmentDate = x.AppointmentDate,
                                Diagnosis = x.Diagnosis,
                                Notes = x.Notes,
                                VisitType = x.VisitType,
                                PrescriptionDetails = x.PrescriptionDetails.Select(pd => new PrescriptionDetailOutDTO
                                {
                                    Id = pd.Id,
                                    AppointmentId = pd.AppointmentId,
                                    MedicineId = pd.MedicineId,
                                    Dosage = pd.Dosage,
                                    Notes = pd.Notes,
                                    StartDate = pd.StartDate,
                                    EndDate = pd.EndDate
                                }).ToList()
                            }).ToList();


            return (response, "data list");

        }


        public async Task<(AppointmentOutDTO, string)> GetByIdAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment is null) return (null!, "data not found");
            var response = new AppointmentOutDTO
            {
                Id = appointment.Id,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Diagnosis = appointment.Diagnosis,
                Notes = appointment.Notes,
                VisitType = appointment.VisitType,
                PrescriptionDetails = appointment.PrescriptionDetails.Select(pd => new PrescriptionDetailOutDTO
                {
                    Id = pd.Id,
                    AppointmentId = pd.AppointmentId,
                    MedicineId = pd.MedicineId,
                    Dosage = pd.Dosage,
                    Notes = pd.Notes,
                    StartDate = pd.StartDate,
                    EndDate = pd.EndDate
                }).ToList()
            };
            return (response, "get data");
        }

        public async Task<(bool, string)> UpdateAppointmentAsync(int id, AppointmentInDTO appointmentInDTO)
        {
            var appointmentFromRepo = await _appointmentRepository.GetByIdAsync(id);
            if (appointmentFromRepo is null)
                return (false, "Data not found");

            appointmentFromRepo.PatientId = appointmentInDTO.PatientId;
            appointmentFromRepo.DoctorId = appointmentInDTO.DoctorId;
            appointmentFromRepo.AppointmentDate = appointmentInDTO.AppointmentDate;
            appointmentFromRepo.Diagnosis = appointmentInDTO.Diagnosis;
            appointmentFromRepo.Notes = appointmentInDTO.Notes;
            appointmentFromRepo.VisitType = appointmentInDTO.VisitType;

            appointmentFromRepo.PrescriptionDetails.Clear();
            foreach (var pd in appointmentInDTO.PrescriptionDetails)
            {
                appointmentFromRepo.PrescriptionDetails.Add(new PrescriptionDetail
                {
                    AppointmentId = appointmentFromRepo.Id,
                    MedicineId = pd.MedicineId,
                    Dosage = pd.Dosage,
                    Notes = pd.Notes,
                    StartDate = pd.StartDate,
                    EndDate = pd.EndDate
                });
            }

            await _appointmentRepository.UpdateAsync(appointmentFromRepo);
            return (true, "Appointment updated successfully");
        }
    }
}