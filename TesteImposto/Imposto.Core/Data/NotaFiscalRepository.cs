using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imposto.Core.Domain;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        SqlConnection _conn = null;
        SqlCommand _cmd = null;

        public NotaFiscalRepository()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["teste"].ConnectionString);
            _conn.Open();

            _cmd = new SqlCommand();
            _cmd.Connection = _conn;
        }

        public void GravaNfe(Domain.NotaFiscal nfe)
        {
            int idNfe = 0;

            _cmd.CommandText = "P_NOTA_FISCAL";
            _cmd.CommandType = System.Data.CommandType.StoredProcedure;

            _cmd.Parameters.Clear();
            _cmd.Parameters.Add(AddParameter("@pId", 0, DbType.Int32, ParameterDirection.Output));
            _cmd.Parameters.Add(AddParameter("@pNumeroNotaFiscal", nfe.NumeroNotaFiscal, DbType.Int32, ParameterDirection.Input));
            _cmd.Parameters.Add(AddParameter("@pSerie", nfe.Serie, DbType.Int32, ParameterDirection.Input));
            _cmd.Parameters.Add(AddParameter("@pNomeCliente", nfe.NomeCliente, DbType.String, ParameterDirection.Input));
            _cmd.Parameters.Add(AddParameter("@pEstadoDestino", nfe.EstadoDestino, DbType.String, ParameterDirection.Input));
            _cmd.Parameters.Add(AddParameter("@pEstadoOrigem", nfe.EstadoOrigem, DbType.String, ParameterDirection.Input));

            _cmd.ExecuteNonQuery();

            idNfe = Convert.ToInt32(_cmd.Parameters["@pId"].Value);

            _cmd.CommandText = "P_NOTA_FISCAL_ITEM";
            _cmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (NotaFiscalItem item in nfe.ItensDaNotaFiscal)
            {
                _cmd.Parameters.Clear();
                _cmd.Parameters.Add(AddParameter("@pId", 0, DbType.Int32, ParameterDirection.Output));
                _cmd.Parameters.Add(AddParameter("@pIdNotaFiscal", idNfe, DbType.Int32, ParameterDirection.Input));
                _cmd.Parameters.Add(AddParameter("@pCfop", item.Cfop, DbType.String, ParameterDirection.Input));
                _cmd.Parameters.Add(AddParameter("@pTipoIcms", item.TipoIcms, DbType.String, ParameterDirection.Input));
                _cmd.Parameters.Add(AddParameter("@pBaseIcms", item.BaseIcms, DbType.Double, ParameterDirection.Input));
                _cmd.Parameters.Add(AddParameter("@pAliquotaIcms", item.AliquotaIcms, DbType.Double, ParameterDirection.Input));
                _cmd.Parameters.Add(AddParameter("@pValorIcms", item.ValorIcms, DbType.Double, ParameterDirection.Input));
                _cmd.Parameters.Add(AddParameter("@pNomeProduto", item.NomeProduto, DbType.String, ParameterDirection.Input));
                _cmd.Parameters.Add(AddParameter("@pCodigoProduto", item.CodigoProduto, DbType.String, ParameterDirection.Input));

                _cmd.Parameters.Add(AddParameter("@pBaseIcms", item.BaseIpi, DbType.Double, ParameterDirection.Input));
                _cmd.Parameters.Add(AddParameter("@pAliquotaIcms", item.AliquotaIpi, DbType.Double, ParameterDirection.Input));
                _cmd.Parameters.Add(AddParameter("@pValorIcms", item.ValorIpi, DbType.Double, ParameterDirection.Input));

                _cmd.Parameters.Add(AddParameter("@pDesconto", item.Desconto, DbType.Double, ParameterDirection.Input));

                _cmd.ExecuteNonQuery();
            }
        }

        private SqlParameter AddParameter(string nomeParametro, object valor, DbType type, ParameterDirection direcao)
        {
            SqlParameter parametro = new SqlParameter(nomeParametro, valor);
            parametro.DbType = type;
            parametro.Direction = direcao;

            return parametro;
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
            }
        }
    }
}
