using System.Data;
using System.Data.SqlClient;
using TesteImposto.Entidade;

namespace TesteImposto.Persistencia
{
    public class NotaFiscalDAO : BaseDAO
    {
        public int Incluir(NotaFiscal notaFiscal)
        {
            var query = "P_NOTA_FISCAL";
            var command = new SqlCommand(query) {CommandType = CommandType.StoredProcedure};

            command.Parameters.AddWithValue("@pId", notaFiscal.Id).Direction = ParameterDirection.InputOutput;
            command.Parameters.AddWithValue("@pNumeroNotaFiscal", notaFiscal.NumeroNotaFiscal);
            command.Parameters.AddWithValue("@pSerie", notaFiscal.Serie);
            command.Parameters.AddWithValue("@pNomeCliente", notaFiscal.NomeCliente);
            command.Parameters.AddWithValue("@pEstadoDestino", notaFiscal.EstadoDestino);
            command.Parameters.AddWithValue("@pEstadoOrigem", notaFiscal.EstadoOrigem);

            this.ExecuteNonQuery(command);

            notaFiscal.Id = (int)command.Parameters["@pId"].Value;

            return notaFiscal.Id;
        }
    }
}