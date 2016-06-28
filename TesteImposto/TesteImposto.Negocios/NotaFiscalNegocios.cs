using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TesteImposto.Dominio.Entidade;
using TesteImposto.Infra;

namespace TesteImposto.Negocios
{
   public class PREPARAXML
    {

        private static NotaFiscal CarregarNotaFiscal(Pedido DadosPedido)
        {
            var notaFiscal = new NotaFiscal
            {
                EstadoOrigem = DadosPedido.EstadoOrigem,
                NomeCliente = DadosPedido.NomeCliente,
                NumeroNotaFiscal = new Random().Next(100000),
                Serie = 1,
                EstadoDestino = DadosPedido.EstadoDestino
            };

            // estados emissores SP e MG
            foreach (var itemPedido in DadosPedido.ItensDoPedido)
            {
                var notaFiscalItem = new NotaFiscalItem();

                if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "RJ"))
                {
                    notaFiscalItem.Cfop = "6.000";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "PE"))
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "MG"))
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "PB"))
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "PR"))
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "PI"))
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "RO"))
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "TO"))
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if ((notaFiscal.EstadoOrigem == "SP") && (notaFiscal.EstadoDestino == "PA"))
                {
                    notaFiscalItem.Cfop = "6.010";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "RJ"))
                {
                    notaFiscalItem.Cfop = "6.000";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "PE"))
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "MG"))
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "PB"))
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "PR"))
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "PI"))
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "RO"))
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "TO"))
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "SE"))
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if ((notaFiscal.EstadoOrigem == "MG") && (notaFiscal.EstadoDestino == "PA"))
                {
                    notaFiscalItem.Cfop = "6.010";
                }

                if (notaFiscal.EstadoDestino == notaFiscal.EstadoOrigem)
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
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido * 0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
                }

                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;

                notaFiscalItem.BaseIpi = itemPedido.ValorItemPedido;
                notaFiscalItem.AliquotaIpi = .10;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;

                    notaFiscalItem.AliquotaIpi = 0;
                }

                //ajuste para estado da regiao sudeste com desconto diferenciado
                var regiaoSudeste = new string[] { "SP", "RJ", "MG", "ES" };

                notaFiscalItem.Desconto = regiaoSudeste.Contains(notaFiscal.EstadoDestino) ? 0 : .10;

                notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;

                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                notaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            return notaFiscal;
        }


        public bool EmitirNota(Pedido pedido)
        {
            if (!VericaDadosNota(pedido)) { return false; }

                var nf = CarregarNotaFiscal(pedido);


                if (SalvaXML(nf))
                {
                    var dadosNota = new DAL();
                    var idNotaFiscal = dadosNota.IncluirNota(nf);

                    if (nf.ItensDaNotaFiscal.Any())
                    {
                        var dadosItemNota = new DAL();

                        foreach (var item in nf.ItensDaNotaFiscal)
                        {
                            item.IdNotaFiscal = idNotaFiscal;
                            dadosItemNota.IncluirItemNota(item);
                        }
                    }
                return true;
            }else { return false; }
            
        }

        private static bool SalvaXML(NotaFiscal notaFiscal)
        {
            var serial = new XmlSerializer(typeof(NotaFiscal));
            TextWriter file = new StreamWriter(Path.Combine(@"../../XML/", notaFiscal.NumeroNotaFiscal 
                + "_" + DateTime.Now.Year + ".xml"));
            serial.Serialize(file, notaFiscal);
            file.Close();

            return true;
        }


     

        public  bool VericaDadosNota(Pedido DadosPedido)
        {
            if (string.IsNullOrEmpty(DadosPedido.EstadoDestino) ||
                 string.IsNullOrEmpty(DadosPedido.EstadoOrigem))
            { return false; }

            if (string.IsNullOrEmpty(DadosPedido.NomeCliente))
            { return false; }

            if (DadosPedido == null)
            { return false; }

            foreach (var x in DadosPedido.ItensDoPedido)
            {
                if (string.IsNullOrEmpty(x.CodigoProduto))
                { return false; }

                if (string.IsNullOrEmpty(x.NomeProduto))
                { return false; }

                if (x.CodigoProduto.Length > 20)
                { return false; }

                if (x.ValorItemPedido <= 0)
                { return false; }
                if (x.NomeProduto.Length > 50)
                { return false; }
            }
            return true;
        }
    }
}
