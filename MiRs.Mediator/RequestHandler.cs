using MediatR;

namespace MiRs.Mediator
{
    /// <summary>
    /// Abstract class that implments shared logic for all Handlers.
    /// </summary>
    /// <typeparam name="TRequest">Object received by hanlder on invocation of handler.</typeparam>
    /// <typeparam name="TResponse">Object returned by handler.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RequestHandler{TRequest, TResponse}"/> class.
    /// </remarks>
    public abstract class RequestHandler<TRequest, TResponse>()
        : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IValidatable
        where TResponse : new()
    {

        /// <summary>
        /// Handler method that run shared logic on every handler request made.
        /// </summary>
        /// <param name="request">Request object for handler.</param>
        /// <param name="cancellationToken">Async cancellation token.</param>
        /// <returns>Object of TResponse which is defined on invocation of handler.</returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            TResponse result = new TResponse();

            request.Validate();

            return await HandleRequest(request, result, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Abstract method used by handlers to plug into shared mediatR logic.
        /// </summary>
        /// <param name="request">Request object defined on invocation of handler.</param>
        /// <param name="result">return object defined on invocation of handler.</param>
        /// <param name="cancellationToken">Async cancellation token.</param>
        /// <returns>Object of TResponse which is defined on invocation of handler.</returns>
        protected abstract Task<TResponse> HandleRequest(TRequest request, TResponse result, CancellationToken cancellationToken);

    }
    
}
