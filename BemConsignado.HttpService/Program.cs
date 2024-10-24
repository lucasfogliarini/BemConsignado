using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Seed();

app.Run();

void Seed()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<BemDbContext>();
    context.CreditAgreements.AddRange(new CreditAgreement()
    {
        MaxLoanAmount = 50000,
        Code = "INSS",
        State = "RS"
    },
    new CreditAgreement()
    {
        MaxLoanAmount = 30000,
        Code = "SIAPE",
        State = "SP"
    });

    context.Agents.AddRange(new Agent
    {
        Name = "AgentActive",
        Cpf = "1",
        IsActive = true,
    },
    new Agent
    {
        Name = "AgentInactive",
        Cpf = "2",
        IsActive = false,
    });
    context.SaveChanges();
}
