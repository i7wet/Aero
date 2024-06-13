//Разработать веб приложение "Редактор рейсов". Фронт - Vue, Бэкэнд - .net8.  
// 
//1. Разработать БД используя подход CodeFirst (EF) 2 таблицы: 
//    - авиакомпании (2х символьный IATA код, наименование) 
//    - рейсы (Дата вылета, номер рейса (текст), авиакомпания(ссылка)) 
//    
//2. Разработать веб страницу для вывода списка всех рейсов отсортированных по дате вылета.  
// 
//3. Реализовать возможности добавления новых рейсов, редактирования существующих 
//    доступны для редактирования следующие поля: Дата вылета - выпадающий календарь,
//    номер рейса - текст (ограничен 10 символами), авиакомпания - выбор из списка авиакомпаний (загружается из справочника). 
//    Допускается реализация как на отдельной странице, так и в отдельном элементе существующей страницы (всплавающее окно, раскрывающийся элемент).  
//   
//Справочник авиакомпаний допускается наполнить вручную 
//Так же допускается использование сторонних UI библиотек Vue

using Aero.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddLocalization();
builder.Services.AddCors(options =>
{
#if  DEBUG
    options.AddDefaultPolicy(
        policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(origin=>true); });
#else
        options.AddDefaultPolicy(
            policy =>
            {
                policy.WithOrigins(builder.Configuration["FrontendAddress"]
                                       .NullGuard("FrontendAddress is required option"))
                    .WithHeaders(
                        "Access-Control-Allow-Origin",
                        "Access-Control-Allow-Headers",
                        "Origin",
                        "Accept",
                        "X-Requested-With",
                        "Content-Type",
                        "Access-Control-Request-Method",
                        "Access-Control-Request-Headers")
                    .WithHeaders(HeaderNames.Authorization);
            });
#endif
});
builder.Services.AddDbContext<AeroDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseCors();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}"
    );

app.Run();