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
        public async Task<IActionResult> GetAsync( )
        {
            var allClients = await _clientService.GetAsync();
            return Ok(allClients);
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetByID(Guid clientId)
        {
            var ExpectedClient = await _clientService.GetByIdAsync(clientId);
            return Ok(ExpectedClient);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Client client)
        {
            await _clientService.SaveAsync(client);
            return Ok("Added client");
        }
    }
}
