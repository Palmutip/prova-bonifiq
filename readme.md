# Prova BonifiQ Backend
Olá!
Este repositório foi criado para participar do processo seletivo de Desenvolvedor Backend da empresa Bonifiq. 

## Parte 1 - Correção do Controller
Na Parte1Controller, o objetivo era corrigir o comportamento do controlador para que ele retornasse números aleatórios diferentes a cada chamada. Para solucionar o problema, eu instanciei a classe Random com o seed no construtor, para que toda vez que a classe for invocada, gerar um novo numero.

## Parte 2 - Paginação e Injeção de Dependência
Na Parte2Controller, foram feitas as seguintes melhorias:

- Corrigi o bug de paginação, garantindo que os resultados sejam retornados corretamente com base no parâmetro page.
- Substituí a instanciação direta do ProductService e CustomerService pela Injeção de Dependência.
- Criada uma nova classe PaginacaoService que retorna os dados paginados dentro de sua propriedade Entites e faz a lógica para as outras propriedades 'TotalCount' e 'HasNext'
- Com a nova classe de Paginação, as classes CustomerList e ProductList foram descartadas.

## Parte 3 - Melhoria na Arquitetura
Na Parte3Controller, realizei uma melhoria na arquitetura para garantir que o princípio Open-Closed seja respeitado. Foi criada uma interface IPaymentMethod para enumerar todas as classes que herdam dessa interface. Através da propridade 'Name' com valor fixo para cada forma de pagamento, é possivel alterar o comportamento individualmente dentro da classe de cada forma de pagamento.

## Parte 4 - Testes Unitários
Na Parte4Controller, criei testes unitários abrangentes para o método CanPurchase da classe CustomerService. Isso garante que as regras de negócio sejam testadas e que o código esteja funcionando conforme o esperado. Foi utilizado o framework de testes xUnit.