using Haustrunk.Application.Artikel.Commands;
using Haustrunk.Application.Artikel.Queries;
using Haustrunk.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Haustrunk.Backend.Controllers
{
    //[Authorize]
    public class ArtikelController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<ArtikelDto>>> Get()
        {
            return await Mediator.Send(new GetAllArtikelQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtikelDto>> Get(Guid id)
        {
            return await Mediator.Send(new GetArtikelQuery { Id = id });

        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateArtikelCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateArtikelCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteArtikelCommand() { Id = id});

            return NoContent();
        }
    }
}
