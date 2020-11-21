using System.ComponentModel.DataAnnotations;

namespace WebMarket.DTO
{
    public class PromocaoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da Promoção é de preenchimento obrigatório")]
        [MinLength(3, ErrorMessage = "Nome da Promoção muito pequeno")]
        [StringLength(100, ErrorMessage = "Nome da Promoção muito grande")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Produto da Promoção é de preenchimento obrigatório")]
        public int ProdutoID { get; set; }

        [Required(ErrorMessage = "Porcentagem da Promoção é de preenchimento obrigatório")]
        [Range(0, 100)]
        public float Porcentagem { get; set; }
    }
}