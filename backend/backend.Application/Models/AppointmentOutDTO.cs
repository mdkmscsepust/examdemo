using backend.Domain.Enums;

namespace backend.Application.Models
{
    public class AppointmentOutDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public VisitType VisitType { get; set; }
        public List<PrescriptionDetailOutDTO> PrescriptionDetails { get; set; } = new();
    }
}