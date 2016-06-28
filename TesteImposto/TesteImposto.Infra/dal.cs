using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteImposto.Dominio.Entidade;

namespace TesteImposto.Infra
{
   public class DAL
    {
        
        protected DbConnection Conexao { get; private set; }

        protected void GerarConexao()
        {
            if (this.Conexao == null)
            {
                this.Conexao = new SqlConnection(@"Server=(localdb)\v11.0;Database=Teste;Trusted_Connection=True;");
            }
        }

        protected void ConectarBanco()
        {
            this.GerarConexao();

            if (this.Conexao.State == ConnectionState.Closed)
            {
                this.Conexao.Open();
            }
        }

        protected void FecharConexao()
        {
            if (this.Conexao != null && this.Conexao.State == ConnectionState.Open)
            {
                this.Conexao.Close();
            }
        }

        protected int ExecuteNonQuery(DbCommand command)
        {
            this.ConectarBanco();

            command.Connection = this.Conexao;

            var resultado = command.ExecuteNonQuery();

            this.FecharConexao();

            return resultado;
        }

        protected DataTable ExecuteDataTable(DbCommand command)
        {
            this.GerarConexao();

            command.Connection = this.Conexao;

            var da = new SqlDataAdapter(command as SqlCommand);
            var dt = new DataTable();

            da.Fill(dt);

            return dt;
        }

        public int IncluirNota(NotaFiscal notaFiscal)
        {
            var query = "P_NOTA_FISCAL";
            var command = new SqlCommand(query) { CommandType = CommandType.StoredProcedure };

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

        public int IncluirItemNota(NotaFiscalItem notaFiscalItem)
        {
            var query = "P_NOTA_FISCAL_ITEM";
            var command = new SqlCommand(query) { CommandType = CommandType.StoredProcedure };

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
