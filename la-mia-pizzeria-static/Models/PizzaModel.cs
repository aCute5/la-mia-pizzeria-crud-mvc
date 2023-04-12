using System.ComponentModel.DataAnnotations;

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
        [Range(18,80, ErrorMessage = "Il prezzo non può superare gli 80 euro")]
        public int Price { get; set; }
       

    }

}
