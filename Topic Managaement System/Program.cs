using TMS.Manager.Contract;
using TMS.Manager.Implementation;
using TMS.Repository.Contract;
using TMS.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDatabase_Utilities, Database_Utilities>();
builder.Services.AddTransient<IUtilities, Utilities>();
builder.Services.AddTransient<IExcelUtilities, ExcelUtilities>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Registration}/{id?}");

app.Run();
