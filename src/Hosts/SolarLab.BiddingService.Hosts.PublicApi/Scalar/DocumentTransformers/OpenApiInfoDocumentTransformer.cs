using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace SolarLab.BiddingService.Hosts.PublicApi.Scalar.DocumentTransformers;

/// <summary>
/// Трансформер Scalar для информации Open API документа.
/// </summary>
public class OpenApiInfoDocumentTransformer : IOpenApiDocumentTransformer
{
    /// <inheritdoc />
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        document.Info = new OpenApiInfo
        {
            Title = "Bidding Service Public API",
            Version = "v1"
        };
        
        return Task.CompletedTask;
    }
}