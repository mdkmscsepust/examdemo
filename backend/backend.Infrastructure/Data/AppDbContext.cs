using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Appointment> Appointments{ get; set; }
        public DbSet<PrescriptionDetail> PrescriptionDetails{ get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors {  get; set; }
        public DbSet<Medicine> Medicines{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patients").HasKey(x => x.Id);
            modelBuilder.Entity<Doctor>().ToTable("Doctors").HasKey(x => x.Id);;
            modelBuilder.Entity<Medicine>().ToTable("Medicines").HasKey(x => x.Id);;

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointments");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.AppointmentDate)
                    .IsRequired();

                entity.Property(x => x.Diagnosis)
                    .HasMaxLength(500);

                entity.Property(x => x.Notes)
                    .HasMaxLength(1000);

                entity.Property(x => x.VisitType)
                    .HasConversion<string>()
                    .IsRequired();
                entity.HasOne(x => x.Patient)
                        .WithMany(p => p.Appointments)
                        .HasForeignKey(x => x.PatientId);
                entity.HasOne(x => x.Doctor)
                        .WithMany(p => p.Appointments)
                        .HasForeignKey(x => x.DoctorId);

                });

            modelBuilder.Entity<PrescriptionDetail>(entity =>
            {
                entity.ToTable("PrescriptionDetails");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Dosage)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.Notes)
                    .HasMaxLength(500);

                entity.Property(x => x.StartDate)
                    .IsRequired();

                entity.Property(x => x.EndDate)
                    .IsRequired();

                entity.HasOne(x => x.Appointment)
                    .WithMany(a => a.PrescriptionDetails)
                    .HasForeignKey(x => x.AppointmentId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.Medicine)
                    .WithOne(a => a.PrescriptionDetail)
                    .HasForeignKey<PrescriptionDetail>(x => x.MedicineId);
            });

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, FullName = "Dr. Alice Smith", CreatedAt = DateTime.UtcNow, CreatedBy=1 },
                new Doctor { Id = 2, FullName = "Dr. John Doe", CreatedAt = DateTime.UtcNow, CreatedBy=1 }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, FullName = "Michael Johnson", ContactNumber = "01700000000", CreatedAt = DateTime.UtcNow, CreatedBy=1 },
                new Patient { Id = 2, FullName = "Sarah Connor", ContactNumber = "01700000000", CreatedAt = DateTime.UtcNow, CreatedBy=1 }
            );

            modelBuilder.Entity<Medicine>().HasData(
                new Medicine { Id = 1, Name = "Paracetamol", Description = "Pain reliever and fever reducer", CreatedAt = DateTime.UtcNow, CreatedBy=1 },
                new Medicine { Id = 2, Name = "Amoxicillin", Description = "Antibiotic for bacterial infections", CreatedAt = DateTime.UtcNow, CreatedBy=1 },
                new Medicine { Id = 3, Name = "Lisinopril", Description = "Used to treat high blood pressure", CreatedAt = DateTime.UtcNow, CreatedBy=1 }
            );

        

            base.OnModelCreating(modelBuilder);
        }
    }
}