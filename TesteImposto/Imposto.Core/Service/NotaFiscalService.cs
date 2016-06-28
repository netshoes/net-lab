using Imposto.Core.Domain;
using Imposto.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Domain.Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(ref pedido);

            NotaFiscalRepository NotaPersist = new NotaFiscalRepository();
            if (NotaPersist.EscreveNotaFiscal(notaFiscal)) {
                NotaPersist.PersisteNotaFiscal(notaFiscal);
            }

        }
    }
}
