using BeautyManager.Data;
using BeautyManager.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();

    // Добавляем услуги, если их нет
    if (!dbContext.Services.Any())
    {
        dbContext.Services.AddRange(
            new Service { Name = "Стрижка женская", Description = "Стрижка любой сложности", Price = 1500, Duration = 60, Category = "Парикмахерские услуги" },
            new Service { Name = "Стрижка мужская", Description = "Классическая стрижка", Price = 1200, Duration = 45, Category = "Парикмахерские услуги" },
            new Service { Name = "Маникюр", Description = "Комплексный уход", Price = 2000, Duration = 90, Category = "Ногтевой сервис" },
            new Service { Name = "Педикюр", Description = "Уход за стопами", Price = 2500, Duration = 120, Category = "Ногтевой сервис" },
            new Service { Name = "Макияж", Description = "Дневной или вечерний", Price = 2000, Duration = 60, Category = "Визаж" },
            new Service { Name = "Массаж лица", Description = "Косметический массаж", Price = 1500, Duration = 45, Category = "Уход за лицом" }
        );
        dbContext.SaveChanges();
    }

    // Добавляем мастеров, если их нет
    if (!dbContext.Masters.Any())
    {
        dbContext.Masters.AddRange(
            new Master { Name = "Анна Петрова", Specialization = "Парикмахер-стилист", Bio = "Опыт работы 8 лет", WorkDays = "ПН-ПТ", WorkHours = "10:00-20:00" },
            new Master { Name = "Елена Соколова", Specialization = "Мастер маникюра", Bio = "Опыт 5 лет", WorkDays = "ПН,СР,ПТ", WorkHours = "11:00-20:00" },
            new Master { Name = "Мария Иванова", Specialization = "Визажист", Bio = "Опыт 6 лет", WorkDays = "ВТ,ЧТ,СБ", WorkHours = "12:00-21:00" }
        );
        dbContext.SaveChanges();
    }
}
app.Run();