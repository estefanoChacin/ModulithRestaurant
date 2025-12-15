using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Orders.Application.Commands.CreateOrder;
using Orders.Application.Commands.DeleteOrder;
using Orders.Application.Commands.DetailsOrder;
using Orders.Application.Queries;

namespace Orders.Application;

public static class OrdersEndpoint
{
    public static IEndpointRouteBuilder Map(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/module/orders").WithTags("Orders");

        group.MapPost("/create", async (CreateOrderCommand request, IMediator mediator) =>
        {
            var result = await mediator.Send(request).ConfigureAwait(false); ;
            return Results.Created("/create", result);
        });

        group.MapPost("/detailsOder", async (DetailsOrderCommand request, IMediator mediator) =>
        {
            var result = await mediator.Send(request).ConfigureAwait(false); ;
            return Results.Ok(result);
        });

        group.MapPost("/delete", async (DeleteOrderCommand request, IMediator mediator) =>
        {
            var result = await mediator.Send(request).ConfigureAwait(false);
            return Results.Ok(result);
        });

        group.MapGet("/listAll", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllOrdersQuery()).ConfigureAwait(false);
            return Results.Ok(result);
        });
        return app;
    }
}
