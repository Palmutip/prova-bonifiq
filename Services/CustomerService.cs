using Microsoft.EntityFrameworkCore;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Commom;
using AutoMapper;
using Castle.Core.Resource;

namespace ProvaPub.Services
{
    /// <summary>
    /// Classe responsável por executar os métodos associados à tabela 'Customers'
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper, ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        #region Public functions

        /// <summary>
        /// Método responsável por retornar uma ViewModel com os dados do cliente que o usuário pode ver.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Customer> GetById(int customerId)
        {
            var customer = _customerRepository.Find(x => x.Id == customerId);

            if (null == customer)
                throw new Exception("Cliente não encontrado.");

            return _mapper.Map<Customer>(customer);
        }

        /// <summary>
        /// Retorna uma lista de clientes com dados paginados
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Retorna os dados da tabela 'Customers' de forma paginada</returns>
        public Paginacao<Customer> ListCustomers(int page)
        {
            return new PaginacaoService<Customer>(_customerRepository.GetAll()).Paginate(page);
        }

        /// <summary>
        /// Método responsável por validar se o Cliente pode realizar uma compra.
        /// </summary>
        /// <param name="customerId">ID do cliente</param>
        /// <param name="purchaseValue">Valor da Compra</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            try
            {
                //Business Rule: Non registered Customers cannot purchase
                await GetById(customerId);

                if (purchaseValue <= 0)
                    throw new Exception("Não é possivel realizar pagamentos com valor R$0,00.");

                ClientePodeComprarApenasUmaVezPorMes(customerId);

                ClienteQueNuncaComprouPodeComprarAte100Reais(customerId, purchaseValue);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Business Rule: A customer can purchase only a single time per month
        /// </summary>
        /// <param name="customerId">ID do cliente</param>
        /// <returns></returns>
        private void ClientePodeComprarApenasUmaVezPorMes(int customerId)
        {
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = _orderRepository.GetAll().Where(s => s.CustomerId == customerId && s.OrderDate >= baseDate)
        .ToList();
            if (ordersInThisMonth.Count > 0)
                throw new Exception("O cliente ja fez uma compra esse mês.");
        }

        /// <summary>
        /// Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
        /// </summary>
        /// <param name="customerId">ID do cliente</param>
        /// <param name="purchaseValue">Valor da Compra</param>
        /// <returns></returns>
        private void ClienteQueNuncaComprouPodeComprarAte100Reais(int customerId, decimal purchaseValue)
        {
            //var have = _customerRepository.co
            var haveBoughtBefore = _customerRepository.GetAll().Where(s => s.Id == customerId && s.Orders.Any()).ToList();
            if (haveBoughtBefore.Count == 0 && purchaseValue > 100)
                throw new Exception("A primeira compra do cliente tem um limite de 100 reais.");
        }

        #endregion
    }
}
