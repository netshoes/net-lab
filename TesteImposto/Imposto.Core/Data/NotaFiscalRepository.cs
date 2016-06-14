using System;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository : IDisposable
    {
        private string _notaFiscalRepositoryPathPattern;
        private TesteImpostoEntities _testeImpostoEntities;

        public NotaFiscalRepository()
        {
            if (string.IsNullOrEmpty(this._notaFiscalRepositoryPathPattern))
            {
                this._notaFiscalRepositoryPathPattern = ConfigurationManager.AppSettings.Get("NotaFiscalRepositoryPathPattern");
            }

            if (this._testeImpostoEntities == null)
            {
                this._testeImpostoEntities = new TesteImpostoEntities();
            }
        }

        public void Dispose()
        {
            this._testeImpostoEntities.Dispose();
            this._testeImpostoEntities = null;
        }

        public void ExportarNotaFiscal(Domain.NotaFiscal notaFiscal)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(notaFiscal.GetType());
            FileStream fileStream = new FileStream(
                String.Format(this._notaFiscalRepositoryPathPattern, Guid.NewGuid()),
                FileMode.Create);
            TextWriter textWriter = new StreamWriter(fileStream, new UTF8Encoding());

            xmlSerializer.Serialize(textWriter, notaFiscal);
            textWriter.Close();
        }

        public void GravarNotaFiscal(Domain.NotaFiscal notaFiscal)
        {
            ObjectParameter pNotaFiscalIdParameter =
                new ObjectParameter("pId", (int)0);

            try
            {
                this._testeImpostoEntities.Database.BeginTransaction();

                this._testeImpostoEntities.P_NOTA_FISCAL(
                    pNotaFiscalIdParameter,
                    notaFiscal.NumeroNotaFiscal,
                    notaFiscal.Serie,
                    notaFiscal.NomeCliente,
                    notaFiscal.EstadoDestino,
                    notaFiscal.EstadoOrigem
                );

                foreach (Domain.NotaFiscalItem notaFiscalItem in notaFiscal.ItensDaNotaFiscal)
                {
                    this._testeImpostoEntities.P_NOTA_FISCAL_ITEM(
                        (int)0,
                        (int)pNotaFiscalIdParameter.Value,
                        notaFiscalItem.Cfop,
                        notaFiscalItem.TipoIcms,
                        (decimal)notaFiscalItem.BaseIcms,
                        (decimal)notaFiscalItem.AliquotaIcms,
                        (decimal)notaFiscalItem.ValorIcms,
                        notaFiscalItem.NomeProduto,
                        notaFiscalItem.CodigoProduto,
                        (decimal)notaFiscalItem.AliquotaIpi,
                        (decimal)notaFiscalItem.ValorIpi,
                        (decimal)notaFiscalItem.Desconto
                    );
                }
            }
            catch (Exception ex)
            {
                this._testeImpostoEntities.Database.CurrentTransaction.Rollback();
            }
            finally
            {
                this._testeImpostoEntities.Database.CurrentTransaction.Commit();
            }
        }
    }
}
