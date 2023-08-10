

using SnakeSpaceBattle.Service.Interfaces;
using SnakeSpaceBattle.Service;
using SnakeSpaceBattle.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IGameService, GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseWebSockets();
app.MapControllers();
app.UseEndpoints(endpoints =>
{

    endpoints.MapHub<GameHub>("/gameHub");
   
});

app.Run();
