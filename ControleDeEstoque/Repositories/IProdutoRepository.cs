using ControleDeEstoque.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeEstoque.Repositories
{
        public interface IProdutoRepository
        {
            Task Cadastrar(Produto p);
            Task Atualizar(Produto p);
            Task Excluir(Produto p);
            Task<IList<Produto>> ObtenhaProdutos();
            Task<Produto> ConsultePorId(int? id);
            Task<Produto> EncontrarPrimeiroProduto(int? id);
            bool ExisteProduto(int id);
            bool ExisteCodigo(double codigo, int id);
        }
}
