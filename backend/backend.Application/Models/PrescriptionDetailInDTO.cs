namespace backend.Application.Models
{
    public class PrescriptionDetailInDTO
    {
        public int MedicineId { get; set; }
        public string Dosage { get; set; } = "2x";
        public string Notes { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}