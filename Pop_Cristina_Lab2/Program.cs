using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// EF Core - leagă contextul de connection string
builder.Services.AddDbContext<Pop_Cristina_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Pop_Cristina_Lab2Context")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
