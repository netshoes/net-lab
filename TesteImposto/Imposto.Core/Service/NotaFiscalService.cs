using Imposto.Core.Data;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public bool GerarNotaFiscal(Domain.Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            NotaFiscalRepository banco = null;
            string xmlFileName = "";
            bool nfeGerada = false;

            try
            {
                notaFiscal.EmitirNotaFiscal(pedido);

                // 23-06-2016[Felipe Trevizan]-> Exercício 1: gerar arquivo xml da nota fiscal
                XmlSerializer notaFiscalXml = new XmlSerializer(typeof(NotaFiscal));
                xmlFileName = "c:\\temp\\nota_fiscal_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                notaFiscalXml.Serialize(new FileStream(xmlFileName, FileMode.OpenOrCreate, FileAccess.Write), notaFiscal);

                if (!File.Exists(xmlFileName))
                    return nfeGerada;

                banco = new NotaFiscalRepository();
                banco.GravaNfe(notaFiscal);

                nfeGerada = true;
            }
            catch (Exception err)
            {
                nfeGerada = false;
            }
            finally
            {
                if (banco != null)
                    banco.Dispose();
            }

            return nfeGerada;
        }
    }
}
