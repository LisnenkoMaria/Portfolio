using Microsoft.EntityFrameworkCore;
using BeautyManager.Models;

namespace BeautyManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // УСЛУГИ
            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Стрижка женская", Description = "Стрижка любой сложности", Price = 1500, Duration = 60, Category = "Парикмахерские услуги" },
                new Service { Id = 2, Name = "Стрижка мужская", Description = "Классическая стрижка", Price = 1200, Duration = 45, Category = "Парикмахерские услуги" },
                new Service { Id = 3, Name = "Маникюр", Description = "Комплексный уход", Price = 2000, Duration = 90, Category = "Ногтевой сервис" },
                new Service { Id = 4, Name = "Педикюр", Description = "Уход за стопами", Price = 2500, Duration = 120, Category = "Ногтевой сервис" },
                new Service { Id = 5, Name = "Макияж", Description = "Дневной или вечерний", Price = 2000, Duration = 60, Category = "Визаж" },
                new Service { Id = 6, Name = "Массаж лица", Description = "Косметический массаж", Price = 1500, Duration = 45, Category = "Уход за лицом" }
            );

            // МАСТЕРА
            modelBuilder.Entity<Master>().HasData(
                new Master { Id = 1, Name = "Анна Петрова", Specialization = "Парикмахер-стилист", Bio = "Опыт работы 8 лет", WorkDays = "ПН-ПТ", WorkHours = "10:00-20:00" },
                new Master { Id = 2, Name = "Елена Соколова", Specialization = "Мастер маникюра", Bio = "Опыт 5 лет", WorkDays = "ПН,СР,ПТ", WorkHours = "11:00-20:00" },
                new Master { Id = 3, Name = "Мария Иванова", Specialization = "Визажист", Bio = "Опыт 6 лет", WorkDays = "ВТ,ЧТ,СБ", WorkHours = "12:00-21:00" }
            );
        }
    }
}