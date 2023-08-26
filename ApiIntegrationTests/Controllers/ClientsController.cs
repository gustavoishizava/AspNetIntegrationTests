using System.Net.Mime;
using ApiIntegrationTests.Data;
using ApiIntegrationTests.Domain;
using ApiIntegrationTests.Infrastructure;
using ApiIntegrationTests.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ApiIntegrationTests.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _repository;
        private readonly IPublisher<Client> _publisher;

        public ClientsController(IClientRepository repository, IPublisher<Client> publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Client))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _repository.GetByIdAsync(id);

            return client is not null ? Ok(client) : NotFound();
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(Client))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        public async Task<IActionResult> Post([FromBody] Client client)
        {
            var exists = await _repository.GetByIdAsync(client.Id);
            if (exists is not null)
                return BadRequest(new ErrorResponse { Error = $"Id={client.Id} already exists." });

            await _repository.AddAsync(client);
            await _publisher.PublishAsync(client);
            return Created($"api/clients/{client.Id}", client);
        }
    }
}
