using Microsoft.EntityFrameworkCore;
using PatientApp.Core.Domain.Common;
using PatientApp.Core.Domain.Entitites;

namespace PatientApp.Infrastructure.Persistence.Contexts
{
    public class PatientAppContext : DbContext
    {
        public PatientAppContext(DbContextOptions<PatientAppContext> options) : base(options) { }

        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<LaboratoryResult> LaboratoryResults { get; set; }
        public DbSet<LaboratoryTest> LaboratoryTests { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = Users.Select(x => x.FirstName).FirstOrDefault();
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = Users.Select(x => x.FirstName).FirstOrDefault();
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables
            modelBuilder.Entity<AccessLevel>()
                .ToTable("AccessLevels");

            modelBuilder.Entity<Appointment>()
                .ToTable("Appointments");

            modelBuilder.Entity<Doctor>()
                .ToTable("Doctors");

            modelBuilder.Entity<LaboratoryResult>()
                .ToTable("LaboratoryResults");

            modelBuilder.Entity<LaboratoryTest>()
                .ToTable("LaboratoryTests");

            modelBuilder.Entity<Patient>()
                .ToTable("Patients");

            modelBuilder.Entity<User>()
                .ToTable("Users");

            #endregion

            #region Primary Keys

            modelBuilder.Entity<AccessLevel>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Appointment>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<Doctor>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<LaboratoryResult>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<LaboratoryResult>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<Patient>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<User>()
               .HasKey(x => x.Id);

            #endregion

            #region Relationships

            modelBuilder.Entity<Doctor>()
                .HasMany<Appointment>(x => x.Appointments)
                .WithOne(x => x.Doctor)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Doctor>()
                .HasMany<LaboratoryResult>(x => x.LaboratoryResults)
                .WithOne(x => x.Doctor)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Doctor>()
                .HasMany<Patient>(x => x.Patients)
                .WithMany(x => x.Doctors);


            modelBuilder.Entity<Patient>()
                .HasMany<Appointment>(x => x.Appointments)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Patient>()
                .HasMany<LaboratoryResult>(x => x.LaboratoryResults)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LaboratoryResult>()
                .HasOne<LaboratoryTest>(x => x.LaboratoryTest)
                .WithMany(x => x.LaboratoryResults)
                .HasForeignKey(x => x.LaboratoryTestId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LaboratoryResult>()
                .HasOne<Appointment>(x => x.Appointment)
                .WithOne(x => x.LaboratoryResult);

            modelBuilder.Entity<AccessLevel>()
                .HasOne<User>(x => x.User)
                .WithOne(x => x.AccessLevel);

            #endregion

            #region Property Configuration

            // Appointment
            modelBuilder.Entity<Appointment>()
                .Property(x => x.Date)
                .IsRequired();
            modelBuilder.Entity<Appointment>()
               .Property(x => x.Cause)
               .HasMaxLength(200)
               .IsRequired();
            modelBuilder.Entity<Appointment>()
               .Property(x => x.Status)
               .IsRequired();

            // Doctor
            modelBuilder.Entity<Doctor>()
               .Property(x => x.FirstName)
               .HasMaxLength(200)
               .IsRequired();
            modelBuilder.Entity<Doctor>()
               .Property(x => x.LastName)
               .HasMaxLength(200)
               .IsRequired();
            modelBuilder.Entity<Doctor>()
               .Property(x => x.Email)
               .HasMaxLength(200)
               .IsRequired();
            modelBuilder.Entity<Doctor>()
               .Property(x => x.Phone)
               .HasMaxLength(60)
               .IsRequired();
            modelBuilder.Entity<Doctor>()
               .Property(x => x.DNI)
               .IsRequired();
            modelBuilder.Entity<Doctor>()
               .Property(x => x.Image)
               .IsRequired();

            // Laboratory Result
            modelBuilder.Entity<LaboratoryResult>()
                .Property(x => x.Result)
                .HasMaxLength(2000)
                .IsRequired();
            modelBuilder.Entity<LaboratoryResult>()
                .Property(x => x.DoctorId)
                .IsRequired();
            modelBuilder.Entity<LaboratoryResult>()
              .Property(x => x.AppointmentId)
              .IsRequired();
            modelBuilder.Entity<LaboratoryResult>()
             .Property(x => x.PatientId)
             .IsRequired();

            // Laboratory Test
            modelBuilder.Entity<LaboratoryTest>()
             .Property(x => x.Name)
             .IsRequired();

            // Patient
            modelBuilder.Entity<Patient>()
               .Property(x => x.FirstName)
               .IsRequired();

            modelBuilder.Entity<Patient>()
              .Property(x => x.LastName)
              .IsRequired();

            modelBuilder.Entity<Patient>()
              .Property(x => x.Email)
              .IsRequired();

            modelBuilder.Entity<Patient>()
              .Property(x => x.Phone)
              .IsRequired();
            modelBuilder.Entity<Patient>()
              .Property(x => x.Address)
              .IsRequired();
            modelBuilder.Entity<Patient>()
              .Property(x => x.BirthDate)
              .IsRequired();
            modelBuilder.Entity<Patient>()
              .Property(x => x.DNI)
              .IsRequired();
            modelBuilder.Entity<Patient>()
              .Property(x => x.IsSmoker)
              .IsRequired();
            modelBuilder.Entity<Patient>()
              .Property(x => x.HasAllergies)
              .IsRequired();
            modelBuilder.Entity<Patient>()
              .Property(x => x.Image)
              .IsRequired();

            // User
            modelBuilder.Entity<User>()
             .Property(x => x.FirstName)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.LastName)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.Email)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.Username)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.Password)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.AccessLevelId)
              .IsRequired();

            // Nivel Access

            modelBuilder.Entity<AccessLevel>()
                .Property(x => x.NumberAccess)
                .IsRequired();

            modelBuilder.Entity<AccessLevel>()
                .Property(x => x.Descripcion)
                .IsRequired();

            #endregion

        }
    }

}
