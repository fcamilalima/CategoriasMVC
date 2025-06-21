using CategoriasMVC.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("CategoriasAPI", options =>
{
    options.BaseAddress = new Uri(builder.Configuration["ServicesUri:Categorias"]); 
});

builder.Services.AddHttpClient("AutenticaAPI", options =>
{
    options.BaseAddress = new Uri(builder.Configuration["ServicesUri:AutenticaAPI"]);
    options.DefaultRequestHeaders.Accept.Clear();
    options.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IAutenticacao, Autenticacao>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
