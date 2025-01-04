using BarberBooking.Common.Helpers;
using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Dto;
using BarberBooking.Models.Models;
using BarberBooking.Operations.BusinessManagers;
using BarberBooking.Operations.BusinessObjects;
using BarberBooking.WebLauncher.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BarberBooking.WebLauncher.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly BarberDbContext _context;
    private readonly ClientManager _clientManager;

    public ClientController(BarberDbContext context)
    {
        _context = context;
        _clientManager = new ClientManager(context);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DtoClient>>> GetClients()
    {
        var clients = await _clientManager.GetClients();
        var result = clients.Select(c =>
        {
            var dtoClient = ObjectCopier.CopyProperties<Client, DtoClient>(c);

            if (c.Appointments != null)
            {
                dtoClient.Appointments = c.Appointments.Select(ObjectCopier.CopyProperties<Appointment, DtoAppointment>).ToList();
            }
            return dtoClient;
        });
        return Ok(result);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<DtoClient>> GetClientById(int id)
    {
        var client = await _clientManager.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound($"Client with ID: {id} not found");
        }

        var result = ObjectCopier.CopyProperties<Client, DtoClient>(client);
        if(client.Appointments != null)
        {
            result.Appointments = client.Appointments
                .Select(a => ObjectCopier.CopyProperties<Appointment, DtoAppointment>(a)).ToList();
        }
        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<DtoClient>> CreateClient(ClientCreateRequest request) 
    {
        var client = ObjectCopier.CopyProperties<ClientCreateRequest, BoClient>(request);
        var createdClient = await _clientManager.CreateClientAsync(client);
        var result = ObjectCopier.CopyProperties<Client, DtoClient>(createdClient);

        return Ok(result);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<DtoClient>> UpdateClient(int id, [FromBody] ClientUpdateRequest request)
    {
        var client = ObjectCopier.CopyProperties<ClientUpdateRequest, BoClient>(request);
        var result = await _clientManager.UpdateClientAsync(id, client);
        if(result == null)
        {
            return NotFound($"Client with ID: {id} not found");
        }
        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteClient(int id)
    {
        var result = await _clientManager.DeleteClientAsync(id);
        if(result == false)
        {
            return NotFound($"Client with ID: {id} not found");
        }
        return Ok(result);
    }
}