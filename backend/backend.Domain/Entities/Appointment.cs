using backend.Domain.Enums;

namespace backend.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public VisitType VisitType { get; set; }
        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();

    }
}