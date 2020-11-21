using System.ComponentModel.DataAnnotations;

namespace WebMarket.DTO
{
    public class ProdutoDTO
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "Nome do Produto é de preenchimento obrigatório")]
        [MinLength(2, ErrorMessage = "Nome do Produto muito pequeno")]
        [StringLength(100, ErrorMessage = "Nome do Produto muito grande")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Categoria do Produto é de preenchimento obrigatório")]
        public int CategoriaID { get; set; }


        [Required(ErrorMessage = "Fornecedor do Produto é de preenchimento obrigatório")]
        public int FornecedorID { get; set; }


        [Required(ErrorMessage = "Preço de custo do Produto é de preenchimento obrigatório")]
        public float PrecoDeCusto { get; set; }


        [Required(ErrorMessage = "Preço de venda do Produto é de preenchimento obrigatório")]
        public string PrecoDeCustoString { get; set; }


        [Required(ErrorMessage = "Preço de venda do Produto é de preenchimento obrigatório")]
        public float PrecoDeVenda { get; set; }


        [Required(ErrorMessage = "Preço de venda do Produto é de preenchimento obrigatório")]
        public string PrecoDeVendaString { get; set; }


        [Required(ErrorMessage = "Medição do Produto é de preenchimento obrigatório")]
        [Range(0, 2, ErrorMessage = "Medição inválida")]
        public int Medicao { get; set; }
    }
}