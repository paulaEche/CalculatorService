using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CalculatorService.Server.Utils
{
    /// <summary>
    /// Class to include tracking id header in Swagger
    /// </summary>
    public class TrackIdHeader : Attribute, IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = CalculatorConstants.TrackingHeader,
                In = ParameterLocation.Header,
                Required = false
            });
        }
    }
}