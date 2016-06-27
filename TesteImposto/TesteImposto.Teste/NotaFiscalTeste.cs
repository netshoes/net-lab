using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TesteImposto.Business;
using TesteImposto.Entidade;

namespace TesteImposto.Teste
{
    [TestClass]
    public class NotaFiscalTeste
    {
        [TestMethod]
        public void TestarIncluir()
        {
            var logica = new NotaFiscalLogica();
            var pedido = new Pedido
            {
                NomeCliente = "Teste Nome Cliente",
                EstadoOrigem = "SP",
                EstadoDestino = "RJ",
                ItensDoPedido = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        NomeProduto = "Teste Nome Produto",
                        CodigoProduto = "1234",
                        ValorItemPedido = 100,
                        Brinde = false
                    }
                }
            };

            logica.Incluir(pedido);
        }
    }
}
