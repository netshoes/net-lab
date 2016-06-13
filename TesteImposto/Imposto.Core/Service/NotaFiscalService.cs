namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Domain.Pedido pedido)
        {
            Domain.NotaFiscal notaFiscal = new Domain.NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);

            using (Data.NotaFiscalRepository notaFiscalRepository = new Data.NotaFiscalRepository())
            {
                notaFiscalRepository.ExportarNotaFiscal(notaFiscal);
                notaFiscalRepository.GravarNotaFiscal(notaFiscal);
            }
        }
    }
}
