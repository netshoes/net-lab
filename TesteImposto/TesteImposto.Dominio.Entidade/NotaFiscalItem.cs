using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteImposto.Dominio.Entidade
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

        public string NomeProduto { get; set; }

        public string CodigoProduto { get; set; }

        public double BaseIpi { get; set; }

        public double AliquotaIpi { get; set; }

        public double ValorIpi { get; set; }

        public double Desconto { get; set; }

    }
}
