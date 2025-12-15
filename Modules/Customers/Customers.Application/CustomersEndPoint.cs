using Customers.Application.Commands.CreateCustomer;
using Customers.Application.Queries.GetAllCustomers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Customers.Application;

public static class CustomersEndPoint
{
    public static IEndpointRouteBuilder Map(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/module/customer").WithTags("customerModule");

        group.MapPost("/create", async (CreateCustomerCommand request, IMediator mediator) =>
        {
            var result = await mediator.Send(request);
            return Results.Created("/create", result);
        });

        group.MapGet("/getAll", async ([FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllCustomerQuery());
            return Results.Ok(result);
        });


        return app;
    }
}
