using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class PedidoItem
    {
        public string NomeProduto { get; set; }
        public string CodigoProduto { get; set; }        
        public double ValorItemPedido { get; set; }
        public bool Brinde { get; set; }        
    }
}
