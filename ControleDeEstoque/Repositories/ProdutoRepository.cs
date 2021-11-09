using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeEstoque.Repositories
{
    public class ProdutoRepository : IDisposable, IProdutoRepository
    {
        private readonly ControleEstoqueContext _contexto;

        public ProdutoRepository(ControleEstoqueContext contexto)
        {
            _contexto = contexto;
        }

        public async Task Cadastrar(Produto p)
        {
            _contexto.Produtos.Add(p);
            await _contexto.SaveChangesAsync();
        }

        public async Task Atualizar(Produto p)
        {
            _contexto.Produtos.Update(p);
            await _contexto.SaveChangesAsync();
        }

        public async Task Excluir(Produto p)
        {
            _contexto.Produtos.Remove(p);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Produto> ConsultePorId(int? id)
        {
            return await _contexto.Produtos.FindAsync(id);
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        public async Task<IList<Produto>> ObtenhaProdutos()
        {
            return await _contexto.Produtos.OrderBy(p => p.Codigo).ToListAsync();
        }

        public async Task<Produto> EncontrarPrimeiroProduto(int? id)
        {
            return await _contexto.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public bool ExisteProduto(int id)
        { 
            return _contexto.Produtos.Any(e => e.Id == id);
        }

        public bool ExisteCodigo(double codigo, int id)
        {
            return _contexto.Produtos.Any(p => p.Codigo == codigo && p.Id != id);
        }
    }
}
