using System.Net.Mime;
using Haseroz.WebApiTemplate.Web.HealthCheck;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Haseroz.WebApiTemplate.Web.Docs.OpenApi;

public class HealthCheckSwaggerFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Components.Schemas.Add(nameof(HealthCheckResponse), new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                ["status"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("string") },
                ["totalDuration"] = new OpenApiSchema { Type = "number", Format = "double", Example = new OpenApiString("0") }
            }
        });

        var tags = new List<OpenApiTag>() { { new() { Name = "Health" } } };

        var responses = new OpenApiResponses
        {
            ["200"] = new OpenApiResponse
            {
                Description = "OK",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    [MediaTypeNames.Application.Json] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.Schema,
                                Id = nameof(HealthCheckResponse)
                            }
                        }
                    }
                }
            }
        };

        swaggerDoc.Paths.Add("/api/health/liveness", new OpenApiPathItem
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
                [OperationType.Get] = new OpenApiOperation
                {
                    Summary = "Returns the API's liveness status",
                    Tags = tags,
                    Responses = responses
                }
            }
        });

        swaggerDoc.Paths.Add("/api/health/readiness", new OpenApiPathItem
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
                [OperationType.Get] = new OpenApiOperation
                {
                    Summary = "Returns the API's readiness status",
                    Tags = tags,
                    Responses = responses
                }
            }
        });
    }
}
