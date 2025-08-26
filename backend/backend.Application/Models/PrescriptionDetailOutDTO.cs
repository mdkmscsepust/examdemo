namespace backend.Application.Models
{
    public class PrescriptionDetailOutDTO
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; } = null!;
        public string Dosage { get; set; } = "2x";
        public string Notes { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}