using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace TesteImposto.Persistencia
{
    public class BaseDAO
    {
        protected DbConnection Conexao { get; private set; }

        protected void CriarConexao()
        {
            if (this.Conexao == null)
            {
                this.Conexao = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Teste;Trusted_Connection=True;");
            }
        }

        protected void AbrirConexao()
        {
            this.CriarConexao();

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
            this.AbrirConexao();

            command.Connection = this.Conexao;

            var resultado = command.ExecuteNonQuery();

            this.FecharConexao();

            return resultado;
        }

        protected DataTable ExecuteDataTable(DbCommand command)
        {
            this.CriarConexao();

            command.Connection = this.Conexao;

            var da = new SqlDataAdapter(command as SqlCommand);
            var dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
    }
}