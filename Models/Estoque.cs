namespace WebMarket.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }
        public float Quantidade { get; set; }
        public bool Status { get; set; }
    }
}