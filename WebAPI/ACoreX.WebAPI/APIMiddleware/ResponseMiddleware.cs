

using ACoreX.Extensions.Types;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ACoreX.WebAPI
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsSwagger(context))
            {
                await _next(context);
            }
            else
            {
                Stream originalBodyStream = context.Response.Body;

                using (MemoryStream responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    try
                    {
                        await _next.Invoke(context);

                        if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            string body = await FormatResponse(context.Response);
                            await HandleSuccessRequestAsync(context, body, context.Response.StatusCode);

                        }
                        else
                        {
                            await HandleNotSuccessRequestAsync(context, context.Response.StatusCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        await HandleExceptionAsync(context, ex);
                    }
                    finally
                    {
                        responseBody.Seek(0, SeekOrigin.Begin);
                        await responseBody.CopyToAsync(originalBodyStream);
                    }
                }
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ApiError apiError = null;
            ApiResponse ApiResponse = null;
            int code = (int)HttpStatusCode.InternalServerError;

            if (exception is CustomException)
            {
                CustomException ex = exception as CustomException;
                apiError = new ApiError(ex.Message)
                {
                    ValidationErrors = ex.Errors,
                    ReferenceErrorCode = ex.Code.ToString(),
                    ReferenceDocumentLink = ex.ReferenceDocumentLink
                };
                context.Response.StatusCode = 500;
            }
            if (exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                code = (int)HttpStatusCode.Unauthorized;
                context.Response.StatusCode = code;
            }
            else
            {
#if !DEBUG
                var msg = "An unhandled error occurred.";  
                string stack = null;  
#else
                string msg = exception.GetBaseException().Message;
                string stack = exception.StackTrace;
#endif

                apiError = new ApiError(msg)
                {
                    Details = stack
                };
                context.Response.StatusCode = code;
            }

            context.Response.ContentType = "application/json";

            ApiResponse = new ApiResponse(null, code, ResponseMessageType.Exception.GetDescription(), apiError);

            string json = JsonConvert.SerializeObject(ApiResponse);

            return context.Response.WriteAsync(json);
        }

        private static Task HandleNotSuccessRequestAsync(HttpContext context, int code)
        {
            context.Response.ContentType = "application/json";

            ApiError apiError = null;
            ApiResponse ApiResponse = null;

            if (code == (int)HttpStatusCode.NotFound)
            {
                apiError = new ApiError("The specified URI does not exist. Please verify and try again.");
            }
            else if (code == (int)HttpStatusCode.NoContent)
            {
                apiError = new ApiError("The specified URI does not contain any content.");
            }
            else
            {
                apiError = new ApiError("Your request cannot be processed. Please contact a support.");
            }

            ApiResponse = new ApiResponse(null, code, ResponseMessageType.Failure.GetDescription(), apiError);
            context.Response.StatusCode = code;

            string json = JsonConvert.SerializeObject(ApiResponse);

            return context.Response.WriteAsync(json);
        }

        private static Task HandleSuccessRequestAsync(HttpContext context, object body, int code)
        {
            context.Response.ContentType = "application/json";
            string jsonString, bodyText = string.Empty;
            ApiResponse ApiResponse = null;


            if (!body.ToString().IsValidJson())
            {
                bodyText = JsonConvert.SerializeObject(body);
            }
            else
            {
                bodyText = body.ToString();
            }

            dynamic bodyContent = JsonConvert.DeserializeObject<dynamic>(bodyText);
            Type type;

            type = bodyContent?.GetType();

            if (type.Equals(typeof(Newtonsoft.Json.Linq.JObject)))
            {
                ApiResponse = JsonConvert.DeserializeObject<ApiResponse>(bodyText);
                if (ApiResponse.StatusCode != code)
                {
                    jsonString = JsonConvert.SerializeObject(ApiResponse);
                }
                else if (ApiResponse.Result != null)
                {
                    jsonString = JsonConvert.SerializeObject(ApiResponse);
                }
                else
                {
                    ApiResponse = new ApiResponse(bodyContent, code);
                    jsonString = JsonConvert.SerializeObject(ApiResponse);
                }
            }
            else
            {
                ApiResponse = new ApiResponse(bodyContent, code);
                jsonString = JsonConvert.SerializeObject(ApiResponse);
            }

            return context.Response.WriteAsync(jsonString);
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string plainBodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return plainBodyText;
        }

        private bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");

        }

    }



}
