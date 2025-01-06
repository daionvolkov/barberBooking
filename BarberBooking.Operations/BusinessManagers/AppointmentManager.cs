using BarberBooking.Common.Enums;
using BarberBooking.Common.Helpers;
using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Models;
using BarberBooking.Operations.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Operations.BusinessManagers
{
    public class AppointmentManager
    {
        private readonly BarberDbContext _context;
        public AppointmentManager(BarberDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            var appointmenta = await _context.Appointments!
           .Include(c => c.ClientSchedules)
           .ToListAsync();
            return appointmenta;
        }


        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            var appointmenta = await _context.Appointments!
           .Include(c => c.ClientSchedules).FirstOrDefaultAsync(a => a.Id == id);
           
            return appointmenta;
        }

        public async Task<Appointment> CreateAppointmentAsync(BoAppointment boAppointment)
        {
            var appointment = ObjectCopier.CopyProperties<BoAppointment, Appointment>(boAppointment);
            var result = await _context.Appointments!.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return result.Entity;
        }


        public async Task<Appointment?> UpdateAppointmentAsync(int id, BoAppointment boAppointment)
        {
            var appointments = await GetAppointmentByIdAsync(id);
            if (appointments == null)
            {
                return null;
            }
            appointments.ServiceDescription = boAppointment.ServiceDescription;
            appointments.ScheduledDate = boAppointment.ScheduledDate;
            var result =  _context.Appointments!.Update(appointments);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }
            _context.Appointments!.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeAppointmentStatusAsync(int id, AppointmentStatusEnum status)
        {
            var appointment = await GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }
            appointment.Status = status.ToString();
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}
