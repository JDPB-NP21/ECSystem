using ECSystem.Server.Main.Controllers;
using ECSystem.Server.Main.Data;
using ECSystem.Server.Main.Helpers;
using ECSystem.Server.Main.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseNpgsql(connectionString);
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication()
    //.AddJwtBearer(options =>
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


builder.Services.AddGrpc();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<LocationService>();
builder.Services.AddSingleton<AuthService>();


//builder.WebHost.UseKestrel(so => {
//    so.ListenLocalhost(8081, op => op.Protocols = HttpProtocols.Http1AndHttp2);
//    so.ListenLocalhost(8085, op => op.Protocols = HttpProtocols.Http2);
//});

var app = builder.Build();

// Use if reverse proxy is present
if ((builder.Configuration.GetValue<string>("UseForwardedHeaders")?.Equals("1")).GetValueOrDefault(false))
    app.UseForwardedHeaders(new ForwardedHeadersOptions {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
} else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//var grpcHost = builder.Configuration.GetValue<string>("GrpcHost") ?? "";
app.MapGrpcService<GreeterService>();
app.MapControllers();
app.MapRazorPages();

app.Run();
