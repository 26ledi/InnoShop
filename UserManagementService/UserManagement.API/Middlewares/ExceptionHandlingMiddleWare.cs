using InnoShop.Exceptions.Shared.Exceptions;

namespace UserManagement.API.Middlewares
{
    /// <summary>
    /// Custon Exception Handler
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of <see cref="ExceptionHandlingMiddleware"/>
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke the middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await WriteExceptionMessageAndStatusCode(context, StatusCodes.Status404NotFound, ex);
            }
            catch (AlreadyExistException ex)
            {
                await WriteExceptionMessageAndStatusCode(context, StatusCodes.Status409Conflict, ex);
            }
        
            catch (Exception ex)
            {
                await WriteExceptionMessageAndStatusCode(context, StatusCodes.Status500InternalServerError, ex);
            }

        }

        /// <summary>
        /// Write the status and the message of the exception in the body of the response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task WriteExceptionMessageAndStatusCode(HttpContext context, int statusCode, Exception ex)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = statusCode,
                TraceId = ex.HResult.ToString(),
                Message = ex.Message,
            });
        }
    }

}
