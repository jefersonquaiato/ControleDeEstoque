using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleDeEstoque.Models;
using ControleDeEstoque.Repositories;

namespace ControleDeEstoque.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _repository;
        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return View(await _repository.ObtenhaProdutos());
        }

        // GET: Produtos/Create
        public IActionResult CadastrarProduto()
        {
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarProduto([Bind("Id,Nome,Codigo,Quantidade")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (CodigoExists(produto.Codigo, produto.Id))
                {
                    TempData["mensagemErro"] = "Código já cadastrado!";
                    return View("CadastrarProduto");
                }

                await _repository.Cadastrar(produto);
                TempData["mensagemSucesso"] = "Produto Cadastrado Com Sucesso!";

                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/AjustarEstoque/5
        public async Task<IActionResult> AjustarEstoque(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _repository.ConsultePorId(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/AjustarEstoque/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjustarEstoque(int id, [Bind("Id,Nome,Codigo,Quantidade")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Atualizar(produto);
                    TempData["mensagemSucesso"] = "Estoque ajustado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/EditarProduto/5
        public async Task<IActionResult> EditarProduto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _repository.ConsultePorId(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/AjustarEstoque/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarProduto(int id, [Bind("Id,Nome,Codigo,Quantidade")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (CodigoExists(produto.Codigo, produto.Id))
                    {
                        TempData["mensagemErro"] = "Código já cadastrado!";
                        return View("EditarProduto");
                    }

                    await _repository.Atualizar(produto);
                    TempData["mensagemSucesso"] = "Produto editado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> ExcluirProduto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _repository.EncontrarPrimeiroProduto(id);
            if (produto == null)
            {
                return NotFound();
            }

            return PartialView(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("ExcluirProduto")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarExclusao(int id)
        {
            var produto = await _repository.ConsultePorId(id);

            await _repository.Excluir(produto);
            TempData["mensagemSucesso"] = "Produto excluído com sucesso!";

            return RedirectToAction(nameof(Index));
        }


        private bool ProdutoExists(int id)
        {
            return _repository.ExisteProduto(id);
        }

        private bool CodigoExists(double codigo, int id)
        {
            return _repository.ExisteCodigo(codigo, id);
        }
    }
}
