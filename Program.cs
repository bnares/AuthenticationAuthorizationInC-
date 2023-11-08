using Microsoft.AspNetCore.Authorization;
using WebApp_Security.CustomAuthorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Denied/NoAccess";
});
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("MustBelongToHRDepartment", policy => policy.RequireClaim("Department", "HR"));
    option.AddPolicy("AdminOnly", policy => policy.RequireClaim( "Admin"));
    option.AddPolicy("HRManager", policy => policy
        .RequireClaim("Department", "HR")
        .RequireClaim("Manager")
        .Requirements.Add(new HRManagerProbationRequirement(3)));
});
//we need to inject HRMANAGERRequirementHandler class to do the custom policy
builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
