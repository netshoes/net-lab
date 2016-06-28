using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Imposto.Core.Domain
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }

        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }

        public void EmitirNotaFiscal(ref Pedido pedido)
        {
           
            this.NumeroNotaFiscal = 99999;
            this.Serie = new Random().Next(Int32.MaxValue);
            this.NomeCliente = pedido.NomeCliente;

            this.EstadoDestino = pedido.EstadoDestino;
            this.EstadoOrigem = pedido.EstadoOrigem;

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();
                if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RJ"))
                {
                    notaFiscalItem.Cfop = "6.000";                    
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PE"))
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "MG"))
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PB"))
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PR"))
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PI"))
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "RO"))
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "TO"))
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if ((this.EstadoOrigem == "SP") && (this.EstadoDestino == "PA"))
                {
                    notaFiscalItem.Cfop = "6.010";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RJ"))
                {
                    notaFiscalItem.Cfop = "6.000";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PE"))
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "MG"))
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PB"))
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PR"))
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PI"))
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "RO"))
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "TO"))
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if ((this.EstadoOrigem == "MG") && (this.EstadoDestino == "PA"))
                {
                    notaFiscalItem.Cfop = "6.010";
                }
                if (this.EstadoDestino == this.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }
                if (notaFiscalItem.Cfop == "6.009")
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido*0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
                }
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms*notaFiscalItem.AliquotaIcms;


                notaFiscalItem.BaseIPI = itemPedido.ValorItemPedido; ;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                    notaFiscalItem.AliquotaIPI = 0;
                }
                else {
                    notaFiscalItem.AliquotaIPI = 0.10;
                }

                if (this.EstadoDestino == "SP" || this.EstadoDestino == "RJ" || this.EstadoDestino == "MG" || this.EstadoDestino == "ES")
                {

                    notaFiscalItem.Desconto = 0.10;
                }
                else {
                    notaFiscalItem.Desconto = notaFiscalItem.Desconto;
                }


                notaFiscalItem.ValorIPI = notaFiscalItem.BaseIPI * notaFiscalItem.AliquotaIPI;
                notaFiscalItem.Desconto = pedido.Desconto;
                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                this.ItensDaNotaFiscal.Add(notaFiscalItem);

            }            
        }
    }
}
