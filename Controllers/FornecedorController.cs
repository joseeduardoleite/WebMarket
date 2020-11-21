using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Data;
using WebMarket.DTO;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly ApplicationDbContext _database;

        public FornecedorController(ApplicationDbContext database)
        {
            this._database = database;
        }

        [HttpPost]
        public IActionResult Salvar(FornecedorDTO fornecedorTemporario)
        {
            if (ModelState.IsValid)
            {
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Nome = fornecedorTemporario.Nome;
                fornecedor.Email = fornecedorTemporario.Email;
                fornecedor.Telefone = fornecedorTemporario.Telefone;
                fornecedor.Status = true;
                _database.Fornecedores.Add(fornecedor);
                _database.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            }
            else
            {
                return View("../Gestao/NovoFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(FornecedorDTO fornecedorTemporario)
        {
            if (ModelState.IsValid)
            {
                var fornecedor = _database.Fornecedores.First(forne => forne.Id == fornecedorTemporario.Id);
                fornecedor.Nome = fornecedorTemporario.Nome;
                fornecedor.Email = fornecedorTemporario.Email;
                fornecedor.Telefone = fornecedorTemporario.Telefone;
                _database.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            }
            else
            {
                return View("../Gestao/EditarFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var fornecedor = _database.Fornecedores.First(forne => forne.Id == id);
                fornecedor.Status = false;
                _database.SaveChanges();
            }
            return RedirectToAction("Fornecedores", "Gestao");
        }
    }
}