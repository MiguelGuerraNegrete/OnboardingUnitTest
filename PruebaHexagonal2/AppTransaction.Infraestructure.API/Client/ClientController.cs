using AppTransaction.Aplication.Interfaces;
using AppTransaction.Domain;
using Microsoft.AspNetCore.Mvc;


namespace AppTransaction.Infraestructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            var allClients = await _clientService.GetAsync();
            return Ok(allClients);
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetByID(Guid clientId , CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            var ExpectedClient = await _clientService.GetByIdAsync(clientId);
            return Ok(ExpectedClient);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Client client, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            await _clientService.SaveAsync(client);
            return Ok("Added client");
        }
    }
}
