using Microsoft.Extensions.Logging;
using Shopping_Cart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Shopping_Cart.Infra.Middleware
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}
		public async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			_logger.LogError(exception, "Sn unexpected error occured");
			ErrorResponse response = exception switch
			{
				ApplicationException => new ErrorResponse(HttpStatusCode.BadRequest, "Application exception occured."),
				DirectoryNotFoundException => new ErrorResponse(HttpStatusCode.NotFound, "There isn't a matching context"),
				_ => new ErrorResponse(HttpStatusCode.InternalServerError, "Internal server error.Please retry later")
			};
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)response.StatusCode;
			var jsonResponse = JsonSerializer.Serialize(response);
			await context.Response.WriteAsync(jsonResponse);
		}
	}
}
