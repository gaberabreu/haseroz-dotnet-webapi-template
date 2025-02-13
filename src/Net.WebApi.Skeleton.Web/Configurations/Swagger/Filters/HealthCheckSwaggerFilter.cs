using System.Net.Mime;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Net.WebApi.Skeleton.Web.Models.HealthCheck;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Net.WebApi.Skeleton.Web.Configurations.Swagger.Filters;

public class HealthCheckSwaggerFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Components.Schemas.Add(nameof(HealthCheckDependency), new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                ["name"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("string") },
                ["status"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("string") },
                ["duration"] = new OpenApiSchema { Type = "number", Format = "double", Example = new OpenApiString("0") },
                ["description"] = new OpenApiSchema { Type = "string", Nullable = true, Example = new OpenApiString("string") },
                ["errorMessage"] = new OpenApiSchema { Type = "string", Nullable = true, Example = new OpenApiString("string") }
            }
        });

        swaggerDoc.Components.Schemas.Add(nameof(HealthCheckResponse), new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                ["status"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("string") },
                ["totalDuration"] = new OpenApiSchema { Type = "number", Format = "double", Example = new OpenApiString("0") },
                ["dependencies"] = new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(HealthCheckDependency) } }
                }
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
            },
            ["503"] = new OpenApiResponse
            {
                Description = "Service Unavailable",
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

        swaggerDoc.Paths.Add("/api/v1/health/liveness", new OpenApiPathItem
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

        swaggerDoc.Paths.Add("/api/v1/health/readiness", new OpenApiPathItem
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
