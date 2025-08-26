namespace backend.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}