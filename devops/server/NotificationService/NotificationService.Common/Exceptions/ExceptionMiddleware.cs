// @Tai.

namespace Common.Exceptions;

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

/// <summary>
/// ExceptionMiddleware to handle exception thrown from controller actions.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">next Delegate object to procress request.</param>
    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// Exception Handling before returning response.
    /// </summary>
    /// <param name="context"> HttpContext object for the curently executing Action.</param>
    /// <param name="exception">Instance of exception thrown. </param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. </returns>
    public static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var message = "Internal Server Error: Something went wrong! please try after sometime.";
        if (exception is ArgumentException || exception is ArgumentNullException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            message = exception.Message;
        }

        return context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = message,
        }.ToString());
    }

    /// <summary>
    /// Entry point for exception handling.
    /// </summary>
    /// <param name="context"> HttpContext object for the curently executing Action.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation. </returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context).ConfigureAwait(false);
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
        {
            var error = $"{nameof(this.InvokeAsync)}: Exception occured for the request : [{context.Request.Path}], Exception: {ex.StackTrace}";
            await HandleExceptionAsync(context, ex).ConfigureAwait(false);
        }
    }
}