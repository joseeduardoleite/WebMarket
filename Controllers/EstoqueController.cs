using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Data;
using WebMarket.DTO;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext _database;

        public EstoqueController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpPost]
        public IActionResult Salvar(EstoqueDTO estoqueTemporario)
        {
            if (ModelState.IsValid)
            {
                Estoque dados = new Estoque();

                dados.Produto = _database.Produtos.First(Produto => Produto.Id == estoqueTemporario.ProdutoID);
                dados.Quantidade = estoqueTemporario.Quantidade;
                dados.Status = true;
                _database.Estoques.Add(dados);
                _database.SaveChanges();

                return RedirectToAction("Estoque", "Gestao");
            }
            else
            {
                ViewBag.Produto = _database.Produtos.ToList();
                return View("../Gestao/NovoEstoque");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(EstoqueDTO dadosTemporario)
        {
            if (ModelState.IsValid)
            {
                var dados = _database.Estoques.First(est => est.Id == dadosTemporario.Id);
                dados.Produto = _database.Produtos.First(pro => pro.Id == dadosTemporario.ProdutoID);
                dados.Quantidade = dadosTemporario.Quantidade;
                _database.SaveChanges();
                return RedirectToAction("Estoque", "Gestao");
            }
            else
            {
                ViewBag.Produto = _database.Produtos.ToList();
                return View("../Gestao/EditarEstoque");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int Id)
        {
            if (Id > 0)
            {
                var dados = _database.Estoques.First(est => est.Id == Id);
                dados.Status = false;
                _database.SaveChanges();
            }
            return RedirectToAction("Estoque", "Gestao");
        }
    }
}



