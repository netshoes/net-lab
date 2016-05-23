using Imposto.Core.Domain.Calculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class NotaFiscalItem
    {
        public int Id { get; set; }
        public int IdNotaFiscal { get; set; }
        public string Cfop { get; set; }
        public string TipoIcms { get; set; }
        public double BaseIcms { get; set; }
        public double AliquotaIcms { get; set; }
        public double ValorIcms { get; set; }
        public double BaseCalculoIpi { get; set; }
        public double AliquotaIpi { get; set; }
        public double ValorIpi { get; set; }
        public double Desconto { get; set; }
        public string NomeProduto { get; set; }
        public string CodigoProduto { get; set; }
        private NotaFiscal NotaFiscal { get; set; }

        public NotaFiscalItem() { }

        public NotaFiscalItem(NotaFiscal nota, string nome, string codigo)
        {
            NotaFiscal = nota;
            NomeProduto = nome;
            CodigoProduto = codigo;
        }

        public void CalculaCfop(string origem, string destino)
        {
            var strategy = new CalculoCfopFactory().Criar(origem, destino);

            Cfop = new CalculadoraCfop(strategy).Calcular();
        }

        public void DefineTipoEAliquotaIcms()
        {
            if (NotaFiscal.EstadoDestino == NotaFiscal.EstadoOrigem)
            {
                TipoIcms = "60";
                AliquotaIcms = 0.18;
            }
            else
            {
                TipoIcms = "10";
                AliquotaIcms = 0.17;
            }
        }

        public void CalculaBaseIcms(double valor)
        {
            if (Cfop == "6.009")
            {
                BaseIcms = valor * 0.90;
            }
            else
            {
                BaseIcms = valor;
            }
        }

        public void CalculaIcms()
        {
            ValorIcms = BaseIcms * AliquotaIcms;
        }

        public void AplicaRegrasParaBrinde()
        {
            TipoIcms = "60";
            AliquotaIcms = 0.18;
            ValorIcms = BaseIcms * AliquotaIcms;
            AliquotaIpi = 0.0;
        }

        public void AplicaDescontoParaSudeste()
        {
            if (NotaFiscal.EstadoDestino.Equals("SP") || NotaFiscal.EstadoDestino.Equals("MG") || NotaFiscal.EstadoDestino.Equals("ES") || NotaFiscal.EstadoDestino.Equals("RJ"))
            {
                Desconto = 0.1;
            }
        }

        public void CalculaIpi()
        {
            ValorIpi = BaseCalculoIpi * AliquotaIpi;            
        }
    }
}
