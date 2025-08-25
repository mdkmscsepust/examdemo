using backend.Domain.Enums;

namespace backend.Application.Models
{
    public class AppointmentInDTO
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public VisitType VisitType { get; set; }
        public List<PrescriptionDetailInDTO> PrescriptionDetails { get; set; } = new();
    }
}