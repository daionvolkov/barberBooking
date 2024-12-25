using BarberBooking.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Models.Dto
{
    public class DtoAppointment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int BarberId { get; set; }
        public ServiceDescriptionEnum ServiceDescription { get; set; }
        public DateTime ScheduledDate { get; set; }
        public AppointmentStatusEnum Status { get; set; } = AppointmentStatusEnum.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
