using BarberBooking.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.DataAccess.AppContext;

public class BarberDbContext : DbContext
{
    public BarberDbContext(DbContextOptions<BarberDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Appointment>? Appointments { get; set; }
    public virtual DbSet<Barber>? Barbers { get; set; }
    public virtual DbSet<Client>? Clients { get; set; }
    public virtual DbSet<ClientSchedule>? ClientSchedules { get; set; }
    public virtual DbSet<WorkSchedule>? WorkSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("appointments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.BarberId).HasColumnName("barber_id");
            entity.Property(e => e.ServiceDescription).HasColumnName("service_description");
            entity.Property(e => e.ScheduledDate)
                .HasColumnName("scheduled_date")
                .IsRequired()
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.Status)
                .HasColumnName("status");
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired()
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasMany(a => a.ClientSchedules)
                .WithOne(cs => cs.Appointment)
                .HasForeignKey(cs => cs.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Barber>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("barbers");
            
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.Specialization).HasColumnName("specialization");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.ExperienceYears).HasColumnName("experience_years");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");
            
            entity.HasMany(b => b.Appointments)
                .WithOne(a => a.Barber)
                .HasForeignKey(a => a.BarberId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasMany(b => b.WorkSchedules)
                .WithOne(a => a.Barber)
                .HasForeignKey(a => a.BarberId)
                .OnDelete(DeleteBehavior.Cascade);


        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");

            entity.HasMany(d => d.Appointments)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ClientSchedule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("client_schedules"); 
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
               .HasColumnName("start_time");
            entity.Property(e => e.EndTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("end_time");

            entity.HasOne(e => e.Appointment)
                .WithMany(a => a.ClientSchedules)
                .HasForeignKey(e => e.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<WorkSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("work_schedules_pkey");
            entity.ToTable("work_schedules"); 
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BarberId).HasColumnName("barber_id");
            entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");
            entity.Property(e => e.StartTime)
                .HasColumnName("start_time")
                .IsRequired()
                .HasColumnType("time"); // Указан тип time для PostgreSQL

            entity.Property(e => e.EndTime)
                .HasColumnName("end_time")
                .IsRequired()
                .HasColumnType("time"); // Указан тип time для PostgreSQL

            //entity.HasOne(w => w.Barber)
            //    .WithMany()
            //    .HasForeignKey(w => w.BarberId)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("work_schedules_barber_id_fkey");
        });

        
        
        OnModelCreatingPartial(modelBuilder);
    }

    private static void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        
    }
}
