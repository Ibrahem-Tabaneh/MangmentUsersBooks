using Bl.Data;
using Bl.IRepository;
using Bl.IRepository.ServicesRepository;
using Bl.Seed;
using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//#region Services Configuration
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<FreeBookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<FreeBookDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin";
    options.AccessDeniedPath = "/Admin/Home/Denied";
});

//#region Repository Services
// Register repository services
builder.Services.AddScoped<IServicesRepository<Category>, ServicesCategory>();
builder.Services.AddScoped<IServicesRepository<SubCategory>, ServicesSubSubCategory>();
builder.Services.AddScoped<IServicesRepository<Book>, ServicesBook>();
builder.Services.AddScoped<IServicesRepositorySlider<Slider>, ServicesSlider>();
builder.Services.AddScoped<IServicesRepositorySetting<Setting>, ServicesSetting>();
builder.Services.AddScoped<IServicesRepositoryPage<Page2>, ServicesPage>();

builder.Services.AddScoped<IServicesRepositoryLog<LogCategory>, ServicesCategoryLog>();
builder.Services.AddScoped<IServicesRepositoryLog<LogSubCategory>, ServicesSubCategoryLog>();
builder.Services.AddScoped<IServicesRepositoryLog<LogBook>, ServicesBookLog>();
//#endregion

//#region Swagger Configuration
// Configure Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Free Books API",
        Description = "API access to category and subcategory and book",
        TermsOfService = new Uri("https://www.linkedin.com/in/ibrahem-tabaneh-249683249/"),
        Contact = new OpenApiContact
        {
            Email = "ibrahemtabaneh@gmail.com",
            Name = "Ibrahem Tabaneh",
            Url = new Uri("https://itlegend.net/CourseVideos/CourseVideos#")
        },
        License = new OpenApiLicense
        {
            Name = "Free Book License",
            Url = new Uri("https://www.linkedin.com/in/ibrahem-tabaneh-249683249/")
        }
    });
    var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var fullPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
    options.IncludeXmlComments(fullPath);
});
//#endregion

var app = builder.Build();

//#region Database Seeding
// Execute the seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await DefaultRole.SeedAsync(roleManager);
        await DefaultUser.SeedDefaultSuperAdmin(userManager);
        await DefaultUser.SeedDefaultCustomer(userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
//#endregion

//#region Middleware Configuration
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//Uncomment the following lines to enable Swagger
/*app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});*/

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Accounts}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//#endregion

app.Run();
