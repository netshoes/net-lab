using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TesteImposto.Negocios;
using TesteImposto.Dominio.Entidade;
using System.Collections.Generic;

namespace TesteImposto.Testeemissao
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GerarNota()
        {
            // geracao de xml e armazenagem da nota

            var xmlENota = new PREPARAXML();
            var dadosPedido = new Pedido
            {
                NomeCliente = "rafael Sanchez",
                NumeroNotaFiscal = 25000,
                Serie = 1,
                EstadoOrigem = "SP",
                EstadoDestino = "TO",

                ItensDoPedido = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        NomeProduto = "produto de teste",
                        CodigoProduto = "2589741",
                        ValorItemPedido = 75.32,
                        Brinde = true
                    }
                }
            };

            xmlENota.EmitirNota(dadosPedido);

        }
    }
}
