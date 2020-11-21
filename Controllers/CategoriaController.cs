using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Data;
using WebMarket.DTO;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _database;

        public CategoriaController(ApplicationDbContext database)
        {
            this._database = database;
        }

        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaTemporaria)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria();
                categoria.Nome = categoriaTemporaria.Nome;
                categoria.Status = true;
                _database.Categorias.Add(categoria);
                _database.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            }
            else
            {
                return View("../Gestao/NovaCategoria");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(CategoriaDTO categoriaTemporaria)
        {
            if (ModelState.IsValid)
            {
                var categoria = _database.Categorias.First(cat => cat.Id == categoriaTemporaria.Id);
                categoria.Nome = categoriaTemporaria.Nome;
                _database.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            }
            else
            {
                return View("../Gestao/EditarCategoria");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var categoria = _database.Categorias.First(cat => cat.Id == id);
                categoria.Status = false;
                _database.SaveChanges();
            }
            return RedirectToAction("Categorias", "Gestao");
        }
    }
}