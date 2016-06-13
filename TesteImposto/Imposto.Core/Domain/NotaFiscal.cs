using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Imposto.Core.Domain
{
    [Serializable()]
    public class NotaFiscal
    {
        private string[] _unidadesFederativasSudeste;

        #region Dicionário CFOP
        private readonly object[] cfopDePara = {
            new object[] { new string[] { "SP", "MG" }, "RJ" , "6.000" },
            new object[] { new string[] { "SP", "MG" }, "PE" , "6.001" },
            new object[] { new string[] { "SP", "MG" }, "MG" , "6.002" },
            new object[] { new string[] { "SP", "MG" }, "PB" , "6.003" },
            new object[] { new string[] { "SP", "MG" }, "PR" , "6.004" },
            new object[] { new string[] { "SP", "MG" }, "PI" , "6.005" },
            new object[] { new string[] { "SP", "MG" }, "RO" , "6.006" },
            new object[] { new string[] { "SP", "MG" }, "SE" , "6.007" },
            new object[] { new string[] { "SP", "MG" }, "TO" , "6.008" },
            new object[] { new string[] { "SP", "MG" }, "SP" , "6.009" },
            new object[] { new string[] { "SP", "MG" }, "PA" , "6.010" }
        };
        #endregion

        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public NotaFiscal()
        {
            this._unidadesFederativasSudeste =
                ConfigurationManager.AppSettings["UnidadesFederativasSudeste"]
                .Split(new char[] { '|' });

            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }

        public void EmitirNotaFiscal(Pedido pedido)
        {
            string cfopItem;

            this.NumeroNotaFiscal = 99999;
            this.Serie = new Random().Next(Int32.MaxValue);
            this.NomeCliente = pedido.NomeCliente;
            this.EstadoOrigem = pedido.EstadoOrigem;
            this.EstadoDestino = pedido.EstadoDestino;

            #region CFOP
            cfopItem = (
                           from object[] cfopDeParaItem in cfopDePara
                           where ((string[])cfopDeParaItem[0]).Contains(this.EstadoOrigem)
                              && ((string)cfopDeParaItem[1]).Equals(this.EstadoDestino)
                           select ((string)cfopDeParaItem[2])
                       )
                       .DefaultIfEmpty(string.Empty)
                       .FirstOrDefault();
            #endregion

            this.ItensDaNotaFiscal.Clear();

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;
                notaFiscalItem.Cfop = cfopItem;

                #region ICMS
                if (
                      this.EstadoOrigem.Equals(this.EstadoDestino)
                   || itemPedido.Brinde
                   )
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }

                notaFiscalItem.BaseIcms = !"6.009".Equals(notaFiscalItem.Cfop)
                                        ? itemPedido.ValorItemPedido
                                        : itemPedido.ValorItemPedido * 0.90; //redução de base

                #region Desconto Base ICMS Região Sudeste
                if (this._unidadesFederativasSudeste.Contains(pedido.EstadoDestino))
                {
                    notaFiscalItem.Desconto = 0.10;

                    notaFiscalItem.BaseIcms *= (1 - notaFiscalItem.Desconto);
                }
                #endregion

                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                #endregion

                #region IPI
                if (!itemPedido.Brinde)
                {
                    notaFiscalItem.AliquotaIpi = 0.10;
                    notaFiscalItem.ValorIpi = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIpi;
                }
                #endregion

                this.ItensDaNotaFiscal.Add(notaFiscalItem);
            }
        }
    }
}
