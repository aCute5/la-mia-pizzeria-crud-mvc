using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using Microsoft.Identity.Client;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using var ctx = new PizzaContext();
            var pizzas = ctx.Pizze.ToArray();

            return View(pizzas);

        }
        public IActionResult Detail(int id)
        {
            using var ctx = new PizzaContext();
            var pizza = ctx.Pizze.SingleOrDefault(p => p.Id == id);

            if (pizza is null)
            {
                return NotFound($"Pizza with id {id} not found.");
            }

            return View(pizza);
        }
        public IActionResult Create ()
        {
            var pizza = new PizzaModel();
            return View(pizza);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaModel data) { 
            if (!ModelState.IsValid)
            {
                return View("Create",data);
            }
            using (PizzaContext context = new PizzaContext())
            {
                PizzaModel pizzatoCreate = new PizzaModel();
                pizzatoCreate.Nome = data.Nome;
                pizzatoCreate.Descrizione = data.Descrizione;
                pizzatoCreate.Price = data.Price;

                context.Pizze.Add(pizzatoCreate);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        
        
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(int id)
		{
			using (var ctx = new PizzaContext())
            {
              var pizza = ctx.Pizze.FirstOrDefault(x => x.Id == id);
                if (pizza == null)
                {
                    return View("Not found");
                }
                else
                {
                    return View(pizza); 
                }
            }
			
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(int id, PizzaModel pizza)
        {
          
            using (PizzaContext ctx = new PizzaContext())
            {
                PizzaModel pizzatoEdit = ctx.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(pizzatoEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    return View("Edit",pizzatoEdit);
                }
            }
        }
    }

}