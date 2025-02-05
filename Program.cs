using AspNetCoreRateLimit;
using Google;
using Voxerra_API.Controllers.ChatHub;
using Voxerra_API.Entities;
using Voxerra_API.Functions.Password;
using Voxerra_API.Functions.Registration;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*",  
            Limit = 100,      
            Period = "1m"     
        }
    };
});
builder.Services.AddInMemoryRateLimiting(); 
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();



builder.WebHost.UseKestrel();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var connectionString = "Server=localhost;port=3306;Database=Voxerra;user=root;password=Skrinal06;";
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
builder.Services.AddDbContext<ChatAppContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddTransient<IUserFunction, UserFunction>();
builder.Services.AddTransient<IUserFriendFunction, UserFriendFunction>();
builder.Services.AddTransient<IMessageFunction, MessageFunction>();
builder.Services.AddTransient<IUserRegistrationFunction, UserRegistrationFunction>();
builder.Services.AddTransient<IEmailFunction, EmailFunction>();
builder.Services.AddTransient<IPasswordFunction, PasswordFunction>();
builder.Services.AddTransient<IFriendAddFunction, FriendAddFunction>();
builder.Services.AddTransient<ISettingFunction, SettingFunction>();
builder.Services.AddScoped<UserOperator>();
builder.Services.AddScoped<ChatHub>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.Urls.Add("https://0.0.0.0:42069");
app.Urls.Add("http://0.0.0.0:42070");


app.UseIpRateLimiting(); 

app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<JwtMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHub<ChatHub>("/ChatHub");

app.Run();
