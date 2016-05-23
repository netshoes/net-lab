using Imposto.Core.Data;
using Imposto.Core.Domain;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        private readonly NotaFiscalRepository _repository;

        public NotaFiscalService()
        {
            _repository = new NotaFiscalRepository();
        }

        public void GerarNotaFiscal(Pedido pedido)
        {
            var nota = new NotaFiscalFactory(pedido).Criar();

            if (_repository.SalvarXml(nota))
            {
                _repository.Salvar(nota);
            }
        }
    }
}
