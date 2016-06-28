using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteImposto.Dominio.Entidade
{
    public class Pedido
    {
        public string NomeCliente { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public List<PedidoItem> ItensDoPedido { get; set; }

        public Pedido()
        {
            ItensDoPedido = new List<PedidoItem>();
        }
    }
}