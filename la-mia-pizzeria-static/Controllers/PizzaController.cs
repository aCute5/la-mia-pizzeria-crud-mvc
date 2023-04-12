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
        [HttpGet]
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
        [HttpGet]
		public IActionResult Update(int id)
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
                PizzaModel pizzatoEdit = context.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();
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
                PizzaModel pizzatoDelete = ctx.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();
                if(pizzatoDelete != null)
                {
                    ctx.Pizze.Remove(pizzatoDelete);
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