namespace backend.Domain.Entities
{
    public class PrescriptionDetail : BaseEntity
    {
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }
        public string Dosage { get; set; } = "2x";
        public string Notes { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Appointment Appointment { get; set; } = null!;
        public Medicine Medicine { get; set; } = null!;
    }
}