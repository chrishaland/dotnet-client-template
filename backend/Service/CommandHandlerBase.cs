using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    [Authorize]
    public abstract class CommandHandlerBase<TRequest> : Controller
    {
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public abstract Task<ActionResult<CommandResponse>> Execute([FromBody]TRequest request, CancellationToken ct);
    }

    [Authorize]
    public abstract class QueryHandlerBase<TRequest, TResponse> : Controller
    {
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public abstract Task<ActionResult<TResponse>> Execute([FromBody] TRequest request, CancellationToken ct);
    }

    public sealed record CommandResponse
    {
        public int? Id { get; }
        public CommandResponse(int? id = null) => (Id) = (id);
    }
}
