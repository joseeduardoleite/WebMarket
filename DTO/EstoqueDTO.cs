using System;
using System.ComponentModel.DataAnnotations;

namespace WebMarket.DTO
{
    public class EstoqueDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProdutoID { get; set; }

        public float Quantidade { get; set; }
    }
}