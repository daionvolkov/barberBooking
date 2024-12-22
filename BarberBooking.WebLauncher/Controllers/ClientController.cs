using BarberBooking.Common.Helpers;
using BarberBooking.DataAccess.AppContext;
using BarberBooking.Models.Dto;
using BarberBooking.Models.Models;
using BarberBooking.Operations.BusinessManagers;
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
    public ActionResult Test()
    {
        return Ok($"server started {DateTime.Now}");
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DtoClient>> GetClientById(int id)
    {
        var client = await _clientManager.GetClientByIdAsync(id);
        if (client == null)
        {
            return BadRequest("Client not found");
        }

        var result = ObjectCopier.CopyProperties<Client, DtoClient>(client);
        return Ok(result);
    }
}