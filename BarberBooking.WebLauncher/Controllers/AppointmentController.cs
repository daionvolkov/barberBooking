using BarberBooking.Common.Enums;
using BarberBooking.Common.Helpers;
using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Dto;
using BarberBooking.Models.Models;
using BarberBooking.Operations.BusinessManagers;
using BarberBooking.Operations.BusinessObjects;
using BarberBooking.WebLauncher.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BarberBooking.WebLauncher.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AppointmentController : Controller
    {
        private readonly BarberDbContext _context;
        private readonly AppointmentManager _appointmentManager;

        public AppointmentController(BarberDbContext context)
        {
            _context = context;
            _appointmentManager = new AppointmentManager(context);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoAppointment>>> GetAppointments()
        {
            var appointments = await _appointmentManager.GetAppointmentsAsync();

            var result = appointments.Select(a =>
            {
                var dtoAppointments = ObjectCopier.CopyProperties<Appointment, DtoAppointment>(a);
               
                if (a.ClientSchedules != null)
                {
                    dtoAppointments.ClientSchedules = a.ClientSchedules.Select(ObjectCopier.CopyProperties<ClientSchedule, DtoClientSchedule>).ToList();
                }

                return dtoAppointments;
            });
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DtoAppointment>> GetClientById(int id)
        {
            var appointment = await _appointmentManager.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound($"Client with ID: {id} not found");
            }

            var result = ObjectCopier.CopyProperties<Appointment, DtoAppointment>(appointment);
            if (appointment.ClientSchedules != null)
            {
                result.ClientSchedules = appointment.ClientSchedules
                    .Select(a => ObjectCopier.CopyProperties<ClientSchedule, DtoClientSchedule>(a)).ToList();
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<DtoAppointment>> CreateAppointment(AppointmentCreateRequest request)
        {
            var isBarberExists = await HumanValidator.ExistsAsync<Barber>(_context, request.BarberId);
            var isClientExists = await HumanValidator.ExistsAsync<Client>(_context, request.ClientId);
            if(!isBarberExists || isClientExists)
            {
                return BadRequest($"Barber with ID: {request.BarberId} or client with ID {request.ClientId} does not exists.");  
            }

            var appointment = ObjectCopier.CopyProperties<AppointmentCreateRequest, BoAppointment>(request);
            var createdAppointment = await _appointmentManager.CreateAppointmentAsync(appointment);
            var result = ObjectCopier.CopyProperties<Appointment, DtoAppointment>(createdAppointment);

            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<DtoAppointment>> UpdateAppointment(int id, [FromBody] AppointmentUpdateRequest request)
        {
            var isBarberExists = await HumanValidator.ExistsAsync<Barber>(_context, request.BarberId);
            if (!isBarberExists)
            {
                return BadRequest($"Barber with ID: {request.BarberId}  does not exists.");
            }
            var appointment = ObjectCopier.CopyProperties<AppointmentUpdateRequest, BoAppointment>(request);
            var result = await _appointmentManager.UpdateAppointmentAsync(id, appointment);
            if (result == null)
            {
                return NotFound($"Appointment with ID: {id} not found");
            }
            return Ok(result);
        }


        [HttpPut("status/{id:int}")]
        public async Task<ActionResult> ChangeAppointmentStatus(int id, [FromQuery] string status)
        {
            string formattedStatus = char.ToUpper(status[0]) + status.Substring(1).ToLower();
            bool isStatusValid = Enum.TryParse<AppointmentStatusEnum>(formattedStatus, true, out _);
            if (isStatusValid)
            {
                BadRequest($"That Status: {status} does not exist");
            }

            AppointmentStatusEnum appStatus = (AppointmentStatusEnum)Enum.Parse(typeof(AppointmentStatusEnum), formattedStatus);
            var result = await _appointmentManager.ChangeAppointmentStatusAsync(id, appStatus);

            return result == true ? Ok($"status has been changed to {status}") : BadRequest("Status unchanged");
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteAppointment(int id)
        {
            var result = await _appointmentManager.DeleteAppointmentAsync(id);
            if (result == false)
            {
                return NotFound($"Appointment with ID: {id} not found");
            }
            return Ok(result);
        }
    }
}
