using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TesteImposto.Entidade;
using TesteImposto.Persistencia;

namespace TesteImposto.Business
{
    public class NotaFiscalLogica
    {
        public void Incluir(Pedido pedido)
        {
            // TODO: Valida se o pedido não está nulo ou inválido
            Validar(pedido);

            //Popula o objeto NotaFiscal com base no pedido.
            var notaFiscal = PopularNotaFiscal(pedido);

            //Gera o arquivo XML na pasta "App_Data" e persiste na base de dados.
            if (GerarXml(notaFiscal))
            {
                var dao = new NotaFiscalDAO();

                //Insere a nota fiscal na tabela.
                var idNotaFiscal = dao.Incluir(notaFiscal);

                //Verifica se há itens da nota fiscal, se existir insere na tabela.
                if (notaFiscal.ItensDaNotaFiscal.Any())
                {
                    var daoItem = new NotaFiscalItemDAO();

                    foreach (var item in notaFiscal.ItensDaNotaFiscal)
                    {
                        item.IdNotaFiscal = idNotaFiscal;
                        daoItem.Incluir(item);
                    }
                }
            }
        }

        #region [ MÉTODOS PRIVADOS ]

        private static void Validar(Pedido pedido)
        {
            if (pedido == null)
                throw new Exception("O pedido não pode ser nulo.");

            if (string.IsNullOrEmpty(pedido.NomeCliente))
                throw new Exception("O nome do cliente não pode ser nulo ou vazio.");

            if (pedido.NomeCliente.Length > 50)
                throw new Exception("O nome do cliente deve ser menor que 50 caracteres.");

            if(string.IsNullOrEmpty(pedido.EstadoDestino) || string.IsNullOrEmpty(pedido.EstadoOrigem))
                throw new Exception("Os estados de destino e origem não podem ser nulos ou vazios.");

            foreach (var item in pedido.ItensDoPedido)
            {
                if (string.IsNullOrEmpty(item.NomeProduto))
                    throw new Exception("O nome do produto não pode ser nulo ou vazio.");

                if (item.NomeProduto.Length > 50)
                    throw new Exception("O nome do produto deve ser menor que 50 caracteres.");

                if (string.IsNullOrEmpty(item.CodigoProduto))
                    throw new Exception("O código do produto não pode ser nulo ou vazio.");

                if (item.CodigoProduto.Length > 20)
                    throw new Exception("O código do produto deve ser menor que 20 caracteres.");

                if (item.ValorItemPedido <= 0)
                    throw new Exception("O valor do produto não pode ser vazio ou zero.");
            }
        }

        private static NotaFiscal PopularNotaFiscal(Pedido pedido)
        {
            var notaFiscal = new NotaFiscal
            {
                NumeroNotaFiscal = 99999,
                Serie = new Random().Next(int.MaxValue),
                NomeCliente = pedido.NomeCliente,
                EstadoDestino = pedido.EstadoDestino,
                EstadoOrigem = pedido.EstadoOrigem
            };

            foreach (var itemPedido in pedido.ItensDoPedido)
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
                notaFiscalItem.AliquotaIpi = 10;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;

                    notaFiscalItem.AliquotaIpi = 0;
                }

                notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;

                //Desconto
                var estadoSudeste = new[] { "ES", "MG", "RJ", "SP" };
                notaFiscalItem.Desconto = estadoSudeste.Contains(notaFiscal.EstadoDestino) ? 0 : 10;

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                notaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            return notaFiscal;
        }

        private static bool GerarXml(NotaFiscal notaFiscal)
        {
            // Cria a instância para a classe XmlSerializer;
            // especificando o tipo de objeto a ser serializado.
            var serializer = new XmlSerializer(typeof(NotaFiscal));
            TextWriter writer = new StreamWriter(Path.Combine(@"../../App_Data/", notaFiscal.Serie + ".xml"));

            // Serializa a entidade NotaFiscal, e fecha o TextWriter.
            serializer.Serialize(writer, notaFiscal);
            writer.Close();

            return true;
        }

        #endregion
    }
}