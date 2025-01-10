
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
    public class WorkScheduleController : Controller
    {
        private readonly BarberDbContext _context;
        private readonly WorkScheduleManager _workScheduleManager;

        public WorkScheduleController(BarberDbContext context)
        {
            _context = context;
            _workScheduleManager = new WorkScheduleManager(context);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoWorkSchedule>>> GetWorkSchedules()
        {
            var workSchedules = await _workScheduleManager.GetWorkSchedulesAsync();
            var result = workSchedules.Select(ws =>
            {
                var dtoworkSchedules = ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>(ws);

               
                return dtoworkSchedules;
            });
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DtoWorkSchedule>> GetWorkScheduleById(int id)
        {
            var workSchedule = await _workScheduleManager.GetWorkScheduleByIdAsync(id);
            if (workSchedule == null)
            {
                return NotFound($"Barber with ID: {id} not found");
            }

            var result = ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>(workSchedule);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DtoWorkSchedule>> CreateWorkSchedule(WorkScheduleRequest request)
        {
            var workSchedule = ObjectCopier.CopyProperties<WorkScheduleRequest, BoWorkSchedule>(request);
            var createdBarber = await _workScheduleManager.CreateWorkScheduleAsync(workSchedule);
            var result = ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>(createdBarber);

            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<DtoBarber>> UpdateWorkSchedule(int id, [FromBody] WorkScheduleRequest request)
        {
            var workSchedule = ObjectCopier.CopyProperties<WorkScheduleRequest, BoWorkSchedule>(request);
            var updatedworkSchedule = await _workScheduleManager.UpdateWorkScheduleAsync(id, workSchedule);
            if (updatedworkSchedule == null)
            {
                return NotFound($"Work Schedule with ID: {id} not found");
            }
            var result = ObjectCopier.CopyProperties<WorkSchedule, DtoWorkSchedule>(updatedworkSchedule);

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteWorkSchedule(int id)
        {
            var result = await _workScheduleManager.DeleteWorkScheduleAsync(id);
            if (result == false)
            {
                return NotFound($"Work Schedule with ID: {id} not found");
            }
            return Ok(result);
        }
    }
}
