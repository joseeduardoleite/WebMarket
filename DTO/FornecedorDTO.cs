using System.ComponentModel.DataAnnotations;

namespace WebMarket.DTO
{
    public class FornecedorDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome de fornecedor é de preenchimento obrigatório")]
        [StringLength(100, ErrorMessage = "Nome de fornecedor muito grande, tente um menor")]
        [MinLength(2, ErrorMessage = "Nome de fornecedor muito pequeno, tente um maior")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail de fornecedor é de preenchimento obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Número de telefone inválido")]
        public string Telefone { get; set; }
    }
}