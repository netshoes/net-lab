using System.Collections.Generic;

namespace TesteImposto.Entidade
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
    }
}
