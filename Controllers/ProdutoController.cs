using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMarket.Data;
using WebMarket.DTO;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _database;

        public ProdutoController(ApplicationDbContext database)
        {
            this._database = database;
        }

        [HttpPost]
        public IActionResult Salvar(ProdutoDTO produtoTemporario)
        {
            if (ModelState.IsValid)
            {
                Produto produto = new Produto();
                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = _database.Categorias.First(categoria => categoria.Id == produtoTemporario.CategoriaID);
                produto.Fornecedor = _database.Fornecedores.First(fornecedor => fornecedor.Id == produtoTemporario.FornecedorID);
                produto.PrecoDeCusto = float.Parse(produtoTemporario.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemporario.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
                produto.Medicao = produtoTemporario.Medicao;
                produto.Status = true;
                _database.Produtos.Add(produto);
                _database.SaveChanges();
                return RedirectToAction("ListarProduto", "Gestao");
            }
            else
            {
                ViewBag.Categorias = _database.Categorias.ToList();
                ViewBag.Fornecedores = _database.Fornecedores.ToList();
                return View("../Gestao/NovoProduto");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(ProdutoDTO produtoTemporario)
        {
            if (ModelState.IsValid)
            {
                var produto = _database.Produtos.First(prod => prod.Id == produtoTemporario.Id);
                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = _database.Categorias.First(categoria => categoria.Id == produtoTemporario.CategoriaID);
                produto.Fornecedor = _database.Fornecedores.First(fornecedor => fornecedor.Id == produtoTemporario.FornecedorID);
                produto.PrecoDeCusto = produtoTemporario.PrecoDeCusto;
                produto.PrecoDeVenda = produtoTemporario.PrecoDeVenda;
                produto.Medicao = produtoTemporario.Medicao;
                _database.SaveChanges();

                return RedirectToAction("ListarProduto", "Gestao");
            }
            else
            {
                return RedirectToAction("ListarProduto", "Gestao");
            }
        }

        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var produto = _database.Produtos.First(prod => prod.Id == id);
                produto.Status = false;
                _database.SaveChanges();
            }
            return RedirectToAction("ListarProduto", "Gestao");
        }

        [HttpPost]
        public IActionResult Produto(int id)
        {
            if (id > 0) {
                var produto = _database.Produtos.Where(p => p.Status == true).Include(prod => prod.Categoria).Include(prod => prod.Fornecedor).First(p => p.Id == id);

                if (produto != null)
                {
                    var estoque = _database.Estoques.First(e => e.Produto.Id == produto.Id);

                    if (estoque == null)
                    {
                        produto = null;
                    }
                }

                if (produto != null)
                {
                    Promocao promocao;
                    try
                    {
                        promocao = _database.Promocoes.First(p => p.Produto.Id == produto.Id && p.Status == true);
                    }
                    catch
                    {
                       return NotFound();
                    }
                    
                    if (promocao != null)
                    {
                        produto.PrecoDeVenda -= (produto.PrecoDeVenda * (promocao.Porcentagem/100));
                    }
                    Response.StatusCode = 200;
                    return Json(produto);
                }
                else
                {
                    Response.StatusCode = 404;
                    return Json(null);
                }
            }
            else {
                Response.StatusCode = 404;
                return Json(null);
            }
        }

        [HttpPost]
        public IActionResult GerarVenda([FromBody] VendaDTO dados)
        {
            Venda venda = new Venda();
            venda.Total = dados.valorTotalDaVenda;
            venda.Troco = dados.valorTroco;
            venda.ValorPago = dados.valorTroco <= 0.01f ? dados.valorTotalDaVenda : dados.valorTotalDaVenda + dados.valorTroco;
            venda.Data = DateTime.Now;
            _database.Vendas.Add(venda);
            _database.SaveChanges();

            List<Saida> saidas = new List<Saida>();
            foreach (var saida in dados.produtos)
            {
                Saida s = new Saida();
                s.Quantidade = saida.quantidade;
                s.ValorDaVenda = saida.subtotal;
                s.Venda = venda;
                s.Produto = _database.Produtos.First(p => p.Id == saida.produto);
                s.Data = DateTime.Now;
                saidas.Add(s);
            }

            _database.AddRange(saidas);
            _database.SaveChanges();

            return Ok(new{ msg="Venda processada com sucesso" });
        }
        

        public class SaidaDTO
        {
            public int produto { get; set; }
            public int quantidade { get; set; }
            public float subtotal { get; set; }
        }

        public class VendaDTO
        {
            public float valorTotalDaVenda { get; set; }
            public float valorTroco { get; set; }
            public SaidaDTO[] produtos { get; set; }
        }
    }
}