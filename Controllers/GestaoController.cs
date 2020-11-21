using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMarket.Data;
using WebMarket.DTO;

namespace WebMarket.Controllers
{
    // [Authorize]
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext _database;

        public GestaoController(ApplicationDbContext database)
        {
            _database = database;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListarCategoria()
        {
            var categorias = _database.Categorias.Where(cat => cat.Status == true).ToList();

            return View(categorias);
        }

        public IActionResult NovaCategoria()
        {
            return View();
        }

        public IActionResult EditarCategoria(int id)
        {
            var categoria = _database.Categorias.First(cat => cat.Id == id);
            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;

            return View(categoriaView);
        }

        public IActionResult ListarFornecedor()
        {
            var fornecedores = _database.Fornecedores.Where(forne => forne.Status == true).ToList();

            return View(fornecedores);
        }

        public IActionResult NovoFornecedor()
        {
            return View();
        }

        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = _database.Fornecedores.First(forne => forne.Id == id);
            FornecedorDTO fornecedorView = new FornecedorDTO();
            fornecedorView.Id = fornecedor.Id;
            fornecedorView.Nome = fornecedor.Nome;
            fornecedorView.Email = fornecedor.Email;
            fornecedorView.Telefone = fornecedor.Telefone;

            return View(fornecedorView);
        }

        public IActionResult ListarProduto()
        {
            var produtos = _database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).Where(p => p.Status == true).ToList();

            return View(produtos);
        }

        public IActionResult NovoProduto()
        {
            ViewBag.Categorias = _database.Categorias.ToList();
            ViewBag.Fornecedores = _database.Fornecedores.ToList();

            return View();
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = _database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).First(prod => prod.Id == id);
            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id = produtoView.Id;
            produtoView.Nome = produto.Nome;
            produtoView.Medicao = produto.Medicao;
            produtoView.PrecoDeCusto = produto.PrecoDeCusto;
            produtoView.PrecoDeVenda = produto.PrecoDeVenda;
            produtoView.CategoriaID = produto.Categoria.Id;
            produtoView.FornecedorID = produto.Fornecedor.Id;

            ViewBag.Categorias = _database.Categorias.ToList();
            ViewBag.Fornecedores = _database.Fornecedores.ToList();

            return View(produtoView);
        }

        public IActionResult ListarPromocao()
        {
            var promocoes = _database.Promocoes.Include(p => p.Produto).Where(x => x.Status == true).ToList();
            return View(promocoes);
        }

        public IActionResult NovaPromocao()
        {
            ViewBag.Produto = _database.Produtos.ToList();
            return View();
        }

        public IActionResult EditarPromocao(int id)
        {
            var promocao = _database.Promocoes.Include(p => p.Produto).First(p => p.Id == id);
            PromocaoDTO promo = new PromocaoDTO();
            promo.Id = promocao.Id;
            promo.Nome = promocao.Nome;
            promo.Porcentagem = promocao.Porcentagem;
            promo.ProdutoID = promocao.Produto.Id;
            ViewBag.Produto = _database.Produtos.ToList();

            return View(promo);
        }

        public IActionResult ListarEstoque()
        {
            var listaEstoque = _database.Estoques.Include(e => e.Produto).ToList();

            return View(listaEstoque);
        }

        public IActionResult NovoEstoque()
        {
            ViewBag.Produto = _database.Produtos.ToList();

            return View();;
        }

        public IActionResult EditarEstoque(int id)
        {
            var estoque = _database.Estoques.Include(p => p.Produto).First(e => e.Id == id);
            ViewBag.Produto = _database.Produtos.ToList();

            EstoqueDTO estoqueView = new EstoqueDTO();
            estoqueView.Id = estoque.Id;
            estoqueView.ProdutoID = estoque.Produto.Id;
            estoqueView.Quantidade = estoque.Quantidade;
            
            return View(estoqueView);
        }

        public IActionResult Vendas()
        {
            var listaDeVendas = _database.Vendas.ToList();
            return View(listaDeVendas);
        }

        [HttpPost]
        public IActionResult RelatorioDeVendas()
        {
            return Ok(_database.Vendas.ToList());
        }
    }
}