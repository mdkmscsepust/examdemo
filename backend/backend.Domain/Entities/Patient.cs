namespace backend.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}