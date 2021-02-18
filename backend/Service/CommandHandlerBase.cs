﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    [Authorize]
    [ApiController]
    public abstract class CommandHandlerBase<TRequest> : ControllerBase
    {
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public abstract Task<ActionResult<CommandResponse>> Execute([FromBody]TRequest request, CancellationToken ct);
    }

    [Authorize]
    [ApiController]
    public abstract class QueryHandlerBase<TRequest, TResponse> : ControllerBase
    {
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public abstract Task<ActionResult<TResponse>> Execute([FromBody]TRequest request, CancellationToken ct);
    }

    public sealed record CommandResponse
    {
        public int? Id { get; }
        public CommandResponse(int? id = null) => (Id) = (id);
    }
}
