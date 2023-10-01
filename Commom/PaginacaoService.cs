using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Commom
{
    /// <summary>
    /// Classe responsável por realizar a paginação dos registros em lista
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginacaoService<T> //: IPaginacaoService<T>
    {
        private readonly IQueryable<T> _queryable;

        public PaginacaoService(IQueryable<T> queryable)
        {
            _queryable = queryable;
        }

        /// <summary>
        /// Retorna um objeto formatado conforme o tipo da entidade
        /// </summary>
        /// <param name="page">Pagina desejada. Se não passar valor, o default do tipo int é 0</param>
        /// <returns></returns>
        public Paginacao<T> Paginate(int page)
        {
            //Define a quantidade total de registros por pagina
            int registrosPorPagina = 10;

            //Se page menos 0 for negativo, a Página Atual é zero, se não ele faz a conta
            int paginaAtual = page - 1 < 0 ? 0 : (page - 1) * registrosPorPagina;

            //Define o valor do Skip para os proximos 10 registros
            int proximaPagina = paginaAtual + 10;

            //Captura a lista de valores da entidade conforme a página atual
            var entidades = _queryable.Skip(paginaAtual).Take(10).ToList();

            //Valida se existe uma próxima pagina a partir
            bool temOutraPagina = _queryable.Skip(proximaPagina).Take(10).ToList().Count() > 0;

            //Calcula o total de registros conforme a consulta da página
            int totalDeRegistros = _queryable.Skip(paginaAtual).Take(10).ToList().Count();

            //Retorna o objeto formatado conforme as páginas
            return new Paginacao<T>
            {
                Entities = entidades,
                TotalCount = totalDeRegistros,
                HasNext = temOutraPagina
            };
        }
    }
}
