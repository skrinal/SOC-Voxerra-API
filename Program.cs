using Voxerra_API.Controllers.ChatHub;
using Voxerra_API.Entities;
using Voxerra_API.Functions.Password;
using Voxerra_API.Functions.Registration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatAppContext>(options =>
{
    options.UseMySql(builder.Configuration["ConnectionString"],
        new MySqlServerVersion(new Version(8, 0, 40)));
});

builder.Services.AddTransient<IUserFunction, UserFunction>();
builder.Services.AddTransient<IUserFriendFunction, UserFriendFunction>();
builder.Services.AddTransient<IMessageFunction, MessageFunction>();
builder.Services.AddTransient<IUserRegistrationFunction, UserRegistrationFunction>();
builder.Services.AddTransient<IEmailFunction, EmailFunction>();
builder.Services.AddTransient<IPasswordFunction, PasswordFunction>();
builder.Services.AddScoped<UserOperator>();
builder.Services.AddScoped<ChatHub>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<JwtMiddleware>();


app.MapControllers();
app.MapHub<ChatHub>("/ChatHub");

app.Run();
