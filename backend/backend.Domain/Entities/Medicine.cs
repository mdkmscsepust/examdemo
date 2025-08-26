namespace backend.Domain.Entities
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = null!;
        public PrescriptionDetail PrescriptionDetail = null!;
    }
}