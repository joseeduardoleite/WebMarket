using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Data;
using WebMarket.DTO;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class PromocaoController : Controller
    {
        private readonly ApplicationDbContext _database;
        
        public PromocaoController(ApplicationDbContext database)
        {
            _database = database;
        }
        
        [HttpPost]
        public IActionResult Salvar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                Promocao promocao = new Promocao();
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = _database.Produtos.First(prod => prod.Id == promocaoTemporaria.ProdutoID);
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                promocao.Status = true;
                _database.Promocoes.Add(promocao);
                _database.SaveChanges();
                return RedirectToAction("ListarPromocao", "Gestao");
            }
            else
            {
                ViewBag.Produtos = _database.Produtos.ToList();
                return View("../Gestao/NovaPromocao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                var promocao = _database.Promocoes.First(p => p.Id == promocaoTemporaria.Id);
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                promocao.Produto = _database.Produtos.First(p => p.Id == promocaoTemporaria.ProdutoID);
                _database.SaveChanges();
                
                return RedirectToAction("ListarPromocao", "Gestao");
            }
            else
            {
                return RedirectToAction("ListarPromocao", "Gestao");
            }
        }

        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var promocao = _database.Promocoes.First(p => p.Id == id);
                promocao.Status = false;
                _database.SaveChanges();
            }
            return RedirectToAction("ListarPromocao", "Gestao");
        }
    }
}