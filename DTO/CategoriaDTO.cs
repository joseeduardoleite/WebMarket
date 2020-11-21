using System.ComponentModel.DataAnnotations;

namespace WebMarket.DTO
{
    public class CategoriaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [StringLength(100, ErrorMessage = "Nome muito grande, tente um menor")]
        [MinLength(2, ErrorMessage = "Nome muito pequeno, tente um maior")]
        public string Nome { get; set; }
    }
}