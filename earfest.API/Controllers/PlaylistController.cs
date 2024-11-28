using earfest.API.Base;
using earfest.API.Features.Playlists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

public class PlaylistController : EarfestBaseController
{
    public PlaylistController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPlaylists()
    {
        return CreateActionResult(await _mediator.Send(new GetPlaylists.Query()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlaylist(string id)
    {
        return CreateActionResult(await _mediator.Send(new GetPlaylistById.Query(id)));
    }
    [HttpPost]
    public async Task<IActionResult> CreatePlaylist([FromBody] CreatePlaylist.Command command)
    {
        return CreateActionResult(await _mediator.Send(command));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlaylist(string id, [FromBody] UpdatePlaylist.Command command)
    {
        var updatedCommand = command with { Id = id };
        var result = await _mediator.Send(updatedCommand);
        return CreateActionResult(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return CreateActionResult(await _mediator.Send(new DeletePlaylist.Command(id)));
    }
}
