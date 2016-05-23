using System;

namespace Imposto.Core.Domain
{
    public class NotaFiscalFactory
    {
        private readonly Pedido _pedido;

        public NotaFiscalFactory(Pedido pedido)
        {
            _pedido = pedido;
        }

        public NotaFiscal Criar()
        {
            var nota = new NotaFiscal();

            nota.NumeroNotaFiscal = new Random().Next(int.MaxValue); //99999;
            nota.Serie = new Random().Next(int.MaxValue);
            nota.NomeCliente = _pedido.NomeCliente;
            nota.EstadoDestino = _pedido.EstadoDestino;
            nota.EstadoOrigem = _pedido.EstadoOrigem;

            foreach (PedidoItem itemPedido in _pedido.ItensDoPedido)
            {
                var notaFiscalItem = new NotaFiscalItem(nota, itemPedido.NomeProduto, itemPedido.CodigoProduto);
                
                notaFiscalItem.CalculaCfop(nota.EstadoOrigem, nota.EstadoDestino);
                notaFiscalItem.DefineTipoEAliquotaIcms();
                notaFiscalItem.CalculaBaseIcms(itemPedido.ValorItemPedido);
                notaFiscalItem.CalculaIcms();                
                notaFiscalItem.BaseCalculoIpi = itemPedido.ValorItemPedido;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.AplicaRegrasParaBrinde();
                }
                else
                {
                    notaFiscalItem.AliquotaIpi = 0.1;
                }

                notaFiscalItem.AplicaDescontoParaSudeste();
                notaFiscalItem.CalculaIpi();

                nota.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            return nota;
        }
    }
}
