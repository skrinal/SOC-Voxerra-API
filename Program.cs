using Voxerra_API.Entities;
using Voxerra_API.Functions.Message;
using Voxerra_API.Functions.UserFriend;
using Voxerra_API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatAppContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionString"]);
});

builder.Services.AddTransient<IUserFunction, UserFunction>();
builder.Services.AddTransient<IUserFriendFunction, UserFriendFunction>();
builder.Services.AddTransient<IMessageFunction, MessageFunction>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
