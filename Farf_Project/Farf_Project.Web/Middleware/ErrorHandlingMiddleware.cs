using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Farf_Project.Core;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Farf_Project.Web
{
    public class ErrorHandlingMiddleware
    {
        #region Private Properties

        private readonly RequestDelegate next;

        private readonly ILogExceptionManager logExceptionManager;
        private readonly IConfiguration configuration;
        #endregion

        #region Constructor

        public ErrorHandlingMiddleware(RequestDelegate next, ILogExceptionManager logExceptionManager, IConfiguration configuration)

        {
            this.next = next;
            this.logExceptionManager= logExceptionManager;
            this.configuration = configuration;
        }
        #endregion
        #region Public Methods

        /// <summary>
        /// Invoke the HttpContext asynchronous.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        #endregion
        #region Private Methods

        /// <summary>
        /// Handle the exceptions asynchronous.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="exception">The Exception.</param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;
            string message = string.Empty;

            if (exception is UnauthorizedException)
            {
                // 401
                code = HttpStatusCode.Unauthorized;
                message = exception.Message;
            }
            else if (exception is EntityNotFoundException)
            {
                // 404
                code = HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else if (exception is MissingArgumentException || exception is InvalidArgumentException)
            {
                // 400
                code = HttpStatusCode.BadRequest;
                message = exception.Message;
            }
            else
            {
                // 500
                
                code = HttpStatusCode.InternalServerError;

                var catchExceptions = Convert.ToBoolean(this.configuration["CatchExceptions"]);

                if (catchExceptions)
                {
                    // log the message and return the exception id
                    var exceptionId = await this.logExceptionManager.StoreLogExceptionAsync(exception);
                    message = $"{exceptionId}";
                }
                else
                {
                    message = exception.ToString();
                }
            }

            var result = JsonConvert.SerializeObject(new { message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
    }
    #endregion
}
