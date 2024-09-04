using parana_bank_credit_offer.Extensions;
using Infra.InfraExtensions;
using Domain.UseCases.Boundaries;
using Infra.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioningExtension();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});
builder.Services.AddSwaggerOptions();
builder.Services.AddConsumers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(InsertClientInput).Assembly));

builder.Services.AddHostedService<CreditOfferConsumer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.AddSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
