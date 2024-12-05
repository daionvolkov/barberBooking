using BarberBooking.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.DataAccess.AppContext;

public class BarberDbContext : DbContext
{
    public BarberDbContext(DbContextOptions<BarberDbContext> options) : base(options)
    {
        
    }
    public DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<Barber> Barbers { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<ClientSchedule> ClientSchedules { get; set; }
    public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }
    
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
            entity.Property(e => e.ScheduledDate).HasColumnName("scheduled_date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            
            modelBuilder.Entity<Appointment>()
                .HasMany(a => a.ClientSchedules)
                .WithOne(cs => cs.Appointment)
                .HasForeignKey(cs => cs.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }


}