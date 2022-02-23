using SuperStore.Funds.Messages;
using SuperStore.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMessaging();


var app = builder.Build();

app.MapGet("/", () => "Funds Service!");
app.MapGet("/message/send/EU/{country}", async (IMessagePublisher messagePuplisher, string country) =>
{
    var message = new FundsMessage(1, 100.00m);
    await messagePuplisher.PublishAsync("Funds", $"EU.{country}", message);
});

app.Run();


