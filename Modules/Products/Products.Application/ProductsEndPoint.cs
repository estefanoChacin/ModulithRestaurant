using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Products.Application.Command;

namespace Products.Application;

public static class ProductsEndPoint
{
    public static IEndpointRouteBuilder Map(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/module/producto").WithTags("productModule");

        group.MapPost("/create", async (CreateProductCommand request, IMediator mediator) =>
        {
            var result = await mediator.Send(request);
            return Results.Created("/create", result);
        });

        return app;
    }
}
