using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Compra
    {
        public int Id { get; set; }
        public int Quantidade { get; internal set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; internal set; }
        public decimal Preco { get; internal set; }

    }
}
