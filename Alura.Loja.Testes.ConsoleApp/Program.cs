using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1 - Testes Iniciais
            //GravarUsandoAdoNet();
            //GravarUsandoEntityFramework();

            //RecuperarProdutosUsandoEntityFramework();
            //ExcluirTodosProdutosUsandoEntityFramework();
            //RecuperarProdutosUsandoEntityFramework();

            //AtualizarProdutoUsandoEntityFramework();
            #endregion

            #region 2- Testes - Update Alternativo e ChangeTracker
            //using (var contexto = new LojaContext())
            //{

            //    //Exibe no console as instruções SQL enviadas pelo EntityFramework
            //    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //    loggerFactory.AddProvider(SqlLoggerProvider.Create());

            //    var produtos = contexto.Produtos.ToList();

            //    ExibeEntries(contexto.ChangeTracker.Entries());

            //    var novoProduto = new Produto()
            //    {
            //        Nome = "Sabão em pó",
            //        Categoria = "Limpeza",
            //        PrecoUnitario = 5.99
            //    };

            //    contexto.Produtos.Add(novoProduto);

            //    contexto.Produtos.Remove(novoProduto);

            //    ExibeEntries(contexto.ChangeTracker.Entries());

            //    contexto.SaveChanges();

            //    ExibeEntries(contexto.ChangeTracker.Entries());

            //    var entry = contexto.Entry(novoProduto);
            //    Console.WriteLine("\n\n" + entry.Entity.ToString() + " - " + entry.State);

            //}
            #endregion

            #region 3 - Testes - EF - Relacionamento 1:N (Produto:Compras)

            /*
             O EF, percebe quando o produto (FK) não existe no BD e insere ele antes de inserir a compra
             */

            //var produto = new Produto {
            //Nome = "Pão",
            //Categoria = "Padaria",
            //PrecoUnitario = 0.4,
            //Unidade = "Unidade"
            //};

            //var compra = new Compra
            //{
            //    Produto = produto,
            //    Quantidade = 6,
            //    Preco = produto.PrecoUnitario * 6
            //};

            //using (var contexto = new LojaContext())
            //{
            //    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //    loggerFactory.AddProvider(SqlLoggerProvider.Create());

            //    contexto.Compras.Add(compra);

            //    contexto.SaveChanges();

            //    ExibeEntries(contexto.ChangeTracker.Entries());
            //}

            #endregion

            #region 4 - Testes - EF - Relacionamento N:N (Promocao:Produto)

            //var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.79, Unidade = "Litros" };
            //var p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 12.45, Unidade = "Gramas" };
            //var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimentos", PrecoUnitario = 4.23, Unidade = "Gramas" };

            //var promocaoDePascoa = new Promocao();
            //promocaoDePascoa.Descricao = "Páscoa Feliz";
            //promocaoDePascoa.DataInicio = DateTime.Now;
            //promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);

            //promocaoDePascoa.IncluiProduto(p1);
            //promocaoDePascoa.IncluiProduto(p2);
            //promocaoDePascoa.IncluiProduto(p3);

            //using (var contexto = new LojaContext())
            //{
            //    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //    loggerFactory.AddProvider(SqlLoggerProvider.Create());

            //    contexto.Promocoes.Add(promocaoDePascoa);

            //    ExibeEntries(contexto.ChangeTracker.Entries());

            //    contexto.SaveChanges();
            //}


            #endregion

            #region 5 - Testes - EF - Relacionamento 1:1 (Cliente:Endereco)
            //var fulano = new Cliente();
            //fulano.Nome = "Fulaninho de Tal";
            //fulano.EnderecoDeEntrega = new Endereco()
            //{
            //    Numero = 12,
            //    Logradouro = "Rua dos Inválidos",
            //    Complemento = "sobrado",
            //    Bairro = "Centro",
            //    Cidade = "Cidade"
            //};

            //using (var contexto = new LojaContext())
            //{
            //    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //    loggerFactory.AddProvider(SqlLoggerProvider.Create());

            //    contexto.Clientes.Add(fulano);
            //    contexto.SaveChanges();
            //    ExibeEntries(contexto.ChangeTracker.Entries());
            //}
            #endregion

            #region 6 - Testes - Relacionamentos - Consultas - Joins/Includes no EF
            /*
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = new Promocao();
                promocao.Descricao = "Queima Total Janeiro 2017";
                promocao.DataInicio = new DateTime(2017, 1, 1);
                promocao.DataTermino = new DateTime(2017, 1, 31);

                var produtos = contexto
                    .Produtos
                    .Where(p => p.Categoria == "Bebidas")
                    .ToList();

                foreach (var item in produtos)
                {
                    promocao.IncluiProduto(item);
                }

                contexto.Promocoes.Add(promocao);
                ExibeEntries(contexto.ChangeTracker.Entries());
                contexto.SaveChanges();
            }

            using (var contexto2 = new LojaContext())
            {
                var serviceProvider = contexto2.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var promocao = contexto2
                    .Promocoes
                    .Include(p => p.Produtos) // "Join"
                    .ThenInclude(pp => pp.Produto)// Para descer mais niveis no relacionamento (List<PromocaoProduto> para Produto)
                    //.Include("Produtos.Produto") //Sobrecarga alternativa para joins, dispensa as anteriores
                    .FirstOrDefault();

                Console.WriteLine("\nMotrando os produtos da promoção...");
                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }

                ExibeEntries(contexto2.ChangeTracker.Entries());
            }
            */
            #endregion

            #region 6.1 - Testes - Relacionamentos - Consultas - Joins/Includes no EF

            /*
             * using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var cliente = contexto
                    .Clientes
                    .Include(c => c.EnderecoDeEntrega)
                    .FirstOrDefault();

                Console.WriteLine($"Endereço de entrega: {cliente.EnderecoDeEntrega.Logradouro}");

                var produto = contexto
                    .Produtos
                    .Include(p => p.Compras)
                    .Where(p => p.Id == 1)
                    .FirstOrDefault();

                //var produtosAcimaDeDezReais = produto.Compras.Where(x => x.Preco > 10);

                //Forma alternativa de filtrar a coleções já carregadas do bd no mesmo contexto
                contexto.Entry(produto)
                 .Collection(p => p.Compras)
                 .Query()
                 .Where(c => c.Produto.Nome == "Pão")
                 //.Where(c => c.ProdutoId == 1)
                 .Load();

                Console.WriteLine($"Mostrando as compras do produto {produto.Nome}");
                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(item.Produto.Nome + " - R$" + item.Preco);
                }



                //ExibeEntries(contexto.ChangeTracker.Entries());
            }
            */
            #endregion

            Console.ReadKey();
        }


        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnitario = 19.89M;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }
        private static void GravarUsandoEntityFramework()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnitario = 19.89M;

            using (var context = new ProdutoDAOEntity())
            {
                context.Adicionar(p);
                //context.SaveChanges();
            }
        }
        private static void RecuperarProdutosUsandoEntityFramework()
        {
            using (var context = new ProdutoDAOEntity())
            {
                var produtos = context.Produtos();

                Console.WriteLine($"Foram encontratos {produtos.Count()} produtos.");

                foreach (var item in produtos)
                {
                    Console.WriteLine(item.Nome);
                }
            }
        }
        private static void ExcluirTodosProdutosUsandoEntityFramework()
        {
            using (var context = new ProdutoDAOEntity())
            {
                var produtos = context.Produtos();
                foreach (var item in produtos)
                {
                    context.Remover(item);
                }
                //context.SaveChanges();
            }
        }
        private static void AtualizarProdutoUsandoEntityFramework()
        {
            GravarUsandoEntityFramework();
            RecuperarProdutosUsandoEntityFramework();

            using (var context = new ProdutoDAOEntity())
            {
                Produto produto = context.Produtos().FirstOrDefault();
                produto.Nome = "Harry Potter ATUALIZADO";
                context.Atualizar(produto);
                //context.SaveChanges();
            }
            RecuperarProdutosUsandoEntityFramework();
        }
        /// <summary>
        /// Exibe no console os comandos SQL gerados pelo EF.
        /// </summary>
        /// <param name="entries"></param>
        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("===================");
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }
    }
}
