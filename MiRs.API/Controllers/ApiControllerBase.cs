using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MiRs.API.Controllers
{
    /// <summary>
    /// Api base controller that gives access to MediatR object
    /// </summary>
    [ApiController]
    [Route("v{version:apiVersion}/[Controller]")]
    [Produces("application/json")]
    public class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;

        /// <summary>
        /// Api base controller that gives access to MediatR object
        /// </summary>
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>() ?? throw new InvalidOperationException("Isender init was unsuccesful");

    }
}
