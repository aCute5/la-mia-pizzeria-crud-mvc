﻿using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaModel
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(25, ErrorMessage = "Il nome non può avere più di 25 caratteri")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(25, ErrorMessage = "Il nome non può avere più di 25 caratteri")]
        public string? Descrizione { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        
        public int Price { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public PizzaModel(string nome, string descrizione, int price)
        {
            Nome = nome;
            Descrizione = descrizione;
            Price = price;
        }
        public PizzaModel() { }
    }

}
