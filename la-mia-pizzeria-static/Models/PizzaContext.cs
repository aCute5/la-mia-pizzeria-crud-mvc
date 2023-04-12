using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaContext : DbContext
    {

    public DbSet<PizzaModel> Pizze { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzaDb;Integrated Security=True; encrypt = False");
        }
        public void Seed()
        {
            if(!Pizze.Any())
            {
                var seed = new PizzaModel[]
                {
                    new PizzaModel
                    {
                        Nome = "Margherita",
                        Descrizione = "Pomodoro Mozzarella",
                        Price = 5,
                    },
                    new PizzaModel
                    {
                        Nome = "Marinara",
                        Descrizione = "Pomodoro Origano",
                        Price = 3,
                    }
                };
                Pizze.AddRange(seed);

                SaveChanges();
            }
        }
    }
}
