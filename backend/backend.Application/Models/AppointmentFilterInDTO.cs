using backend.Domain.Enums;

namespace backend.Application.Models
{
    public class AppointmentFilterInDTO
    {
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public VisitType? VisitType { get; set; }  // Enum
        public DateTime? FromDate { get; set; }    // Filter start date
        public DateTime? ToDate { get; set; }      // Filter end date
        public string? Diagnosis { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}