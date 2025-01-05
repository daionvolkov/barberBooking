using BarberBooking.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Operations.BusinessObjects
{
    public class BoAppointment
    {
        public int ClientId { get; set; }
        public int BarberId { get; set; }
        public string ServiceDescription { get; set; } = null!;
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; } = AppointmentStatusEnum.Pending.ToString();
     
    }
}
