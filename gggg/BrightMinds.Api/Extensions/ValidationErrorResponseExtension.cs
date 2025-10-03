
using BrightMinds.Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BrightMinds.Api.Extensions
{
    public static class ValidationErrorResponseExtension
    {
        public static IServiceCollection AddValidationErrorResponseHelper(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = (Actionresult) =>
                {

                    var errors = Actionresult.ModelState.Where(p => p.Value.Errors.Count() > 0).
                                                                      SelectMany(p => p.Value.Errors)
                                                                      .Select(E => E.ErrorMessage).
                                                                      ToArray();
                    var errorresponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorresponse);
                };
            });

            return services;
        }
    }
}
