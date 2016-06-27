using System.Data;
using System.Data.SqlClient;
using TesteImposto.Entidade;

namespace TesteImposto.Persistencia
{
    public class NotaFiscalItemDAO : BaseDAO
    {
        public int Incluir(NotaFiscalItem notaFiscalItem)
        {
            var query = "P_NOTA_FISCAL_ITEM";
            var command = new SqlCommand(query) {CommandType = CommandType.StoredProcedure};

            command.Parameters.AddWithValue("@pId", notaFiscalItem.Id).Direction = ParameterDirection.InputOutput;
            command.Parameters.AddWithValue("@pIdNotaFiscal", notaFiscalItem.IdNotaFiscal);
            command.Parameters.AddWithValue("@pCfop", notaFiscalItem.Cfop);
            command.Parameters.AddWithValue("@pTipoIcms", notaFiscalItem.TipoIcms);
            command.Parameters.AddWithValue("@pBaseIcms", notaFiscalItem.BaseIcms);
            command.Parameters.AddWithValue("@pAliquotaIcms", notaFiscalItem.AliquotaIcms);
            command.Parameters.AddWithValue("@pValorIcms", notaFiscalItem.ValorIcms);
            command.Parameters.AddWithValue("@pBaseIpi", notaFiscalItem.BaseIpi);
            command.Parameters.AddWithValue("@pAliquotaIpi", notaFiscalItem.AliquotaIpi);
            command.Parameters.AddWithValue("@pValorIpi", notaFiscalItem.ValorIpi);
            command.Parameters.AddWithValue("@pDesconto", notaFiscalItem.Desconto);
            command.Parameters.AddWithValue("@pNomeProduto", notaFiscalItem.NomeProduto);
            command.Parameters.AddWithValue("@pCodigoProduto", notaFiscalItem.CodigoProduto);

            this.ExecuteNonQuery(command);

            notaFiscalItem.Id = (int)command.Parameters["@pId"].Value;

            return notaFiscalItem.Id;
        }
    }
}