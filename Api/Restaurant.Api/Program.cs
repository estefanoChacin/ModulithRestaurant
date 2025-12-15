using Customers.Application;
using Notifications.Application.Events;
using Orders.Application;
using Products.Application;
using Restaurant.Api.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Iniciando Restaurant API...");

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    builder.Services.AddSwagger();
    builder.Services.AddAppConfigKeyVault(builder.Configuration);
    builder.Services.AddServicesModules(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(CustomersEndPoint).Assembly);
        cfg.RegisterServicesFromAssembly(typeof(ProductsEndPoint).Assembly);
        cfg.RegisterServicesFromAssembly(typeof(OrdersEndpoint).Assembly);
        cfg.RegisterServicesFromAssembly(typeof(CreateNotificationEventHandler).Assembly);
    });
    builder.Host.UseSerilog();

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
            c.RoutePrefix = string.Empty;
        });
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();

    ProductsEndPoint.Map(app);
    CustomersEndPoint.Map(app);
    OrdersEndpoint.Map(app);
    app.MapControllers();

    Log.Information("Aplicación configurada. Iniciando servidor...");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación falló al iniciar");
    throw;
}
finally
{
    Log.CloseAndFlush();
}