using System.Net;

namespace WebApplication1.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly ILogger<ExceptionHandlerMiddleware> logger;
		private readonly RequestDelegate next;

		public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
			RequestDelegate next)
		{
			this.logger = logger;
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				var errorId = Guid.NewGuid();

				// Log this expection
				logger.LogError(ex, $"{errorId} ex.Message");

				// Return a custom error response
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";

				var errorResponse = new
				{
					Message = "An unexpected error occurred. Please try again later.",
					ErrorId = errorId
				};

				await context.Response.WriteAsJsonAsync(errorResponse);
			}
		}
	}
}
