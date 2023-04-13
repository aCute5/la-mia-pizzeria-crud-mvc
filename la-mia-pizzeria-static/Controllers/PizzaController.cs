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
            var pizzas = ctx.Pizzas.ToArray();

            return View(pizzas);

        }
        public IActionResult Detail(int id)
        {
            using var ctx = new PizzaContext();
            var pizza = ctx.Pizzas.SingleOrDefault(p => p.Id == id);

            if (pizza is null)
            {
                return NotFound($"Pizza with id {id} not found.");
            }

            return View(pizza);
        }
        [HttpGet]
        public IActionResult Create ()
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                List<Category> categorie = ctx.Categorie.ToList();
                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new PizzaModel();
                model.Categories = categorie;

                return View("Create", model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data) { 
            if (!ModelState.IsValid)
            {
                using (PizzaContext ctx = new PizzaContext())
                {
                    List<Category> categorie = ctx.Categorie.ToList();
                    data.Categories = categorie;
                    return View("Create", data);
                }
            }
            using (PizzaContext context = new PizzaContext())
            {
                PizzaModel pizzatoCreate = new PizzaModel();
                pizzatoCreate.Nome = data.Pizza.Nome;
                pizzatoCreate.Descrizione = data.Pizza.Descrizione;
                pizzatoCreate.Price = data.Pizza.Price;

                context.Pizzas.Add(pizzatoCreate);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        
        
        }
        [HttpGet]
		public IActionResult Update(int id)
		{
			using (PizzaContext ctx = new PizzaContext())  
            {
              PizzaModel pizzatoEdit = ctx.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
            if(pizzatoEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(pizzatoEdit);
                }
            }
			
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(int id, PizzaModel data)
        {
          
            if(!ModelState.IsValid)
            {
                return View("Update", data);
            }
            using (PizzaContext context = new PizzaContext())
            {
                PizzaModel pizzatoEdit = context.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzatoEdit != null)
                {
                    pizzatoEdit.Nome = data.Nome;
                    pizzatoEdit.Descrizione = data.Descrizione;
                    pizzatoEdit.Price = data.Price;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
        public IActionResult Delete ( int id)
        {
            using (PizzaContext ctx = new PizzaContext())
            {
                PizzaModel pizzatoDelete = ctx.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
                if(pizzatoDelete != null)
                {
                    ctx.Pizzas.Remove(pizzatoDelete);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }

			}
        }
    }

}