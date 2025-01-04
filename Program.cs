using Microsoft.EntityFrameworkCore.Internal;
using Voxerra_API.Controllers.ChatHub;
using Voxerra_API.Entities;
using Voxerra_API.Functions.Message;
using Voxerra_API.Functions.Registration;
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
    options.UseMySql(builder.Configuration["ConnectionString"],
        new MySqlServerVersion(new Version(8, 0, 21)));

});


builder.Services.AddTransient<IUserFunction, UserFunction>();
builder.Services.AddTransient<IUserFriendFunction, UserFriendFunction>();
builder.Services.AddTransient<IMessageFunction, MessageFunction>();
builder.Services.AddTransient<IUserRegistrationFunction, UserRegistrationFunction>();
builder.Services.AddScoped<UserOperator>();
builder.Services.AddScoped<EmailMessage>();
builder.Services.AddScoped<ChatHub>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<JwtMiddleware>();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/ChatHub");
});

//app.MapControllers();
//app.MapHub<ChatHub>("/ChatHub");

app.Run();
