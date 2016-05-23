using System.Configuration;
using Imposto.Core.Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        public bool SalvarXml(NotaFiscal nota)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(NotaFiscal));

                using (var writer = new StreamWriter(ConfigurationManager.AppSettings["EnderecoXML"].ToString()))
                {
                    serializer.Serialize(writer, nota);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void Salvar(NotaFiscal nota)
        {
            using (SqlConnection connection = new SqlConnection("Server=NOTE-ANDRE;Database=Teste;Initial Catalog=Teste;Persist Security Info=True;Integrated Security=true;MultipleActiveResultSets=True"))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[P_NOTA_FISCAL]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@pNumeroNotaFiscal", SqlDbType.Int).Value = nota.NumeroNotaFiscal;
                    cmd.Parameters.AddWithValue("@pSerie", SqlDbType.Int).Value = nota.Serie;
                    cmd.Parameters.AddWithValue("@pNomeCliente", SqlDbType.VarChar).Value = nota.NomeCliente;
                    cmd.Parameters.AddWithValue("@pEstadoDestino", SqlDbType.VarChar).Value = nota.EstadoDestino;
                    cmd.Parameters.AddWithValue("@pEstadoOrigem", SqlDbType.VarChar).Value = nota.EstadoOrigem;
                    
                    SqlParameter outPutParameter = new SqlParameter();
                    outPutParameter.ParameterName = "@pId";
                    outPutParameter.SqlDbType = SqlDbType.Int;
                    outPutParameter.Direction = ParameterDirection.InputOutput;
                    outPutParameter.Value = nota.Id;
                    cmd.Parameters.Add(outPutParameter);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    var id = cmd.Parameters["@pId"].Value;

                    if (nota.ItensDaNotaFiscal != null && nota.ItensDaNotaFiscal.Any())
                    {
                        foreach (var item in nota.ItensDaNotaFiscal)
                        {
                            item.IdNotaFiscal = (int)id;
                            SalvarItem(item, connection);
                        }
                    }
                }
            }
        }

        private void SalvarItem(NotaFiscalItem item, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[P_NOTA_FISCAL_ITEM]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pId", SqlDbType.Int).Value = item.Id;
                cmd.Parameters.AddWithValue("@pIdNotaFiscal", SqlDbType.Int).Value = item.IdNotaFiscal;
                cmd.Parameters.AddWithValue("@pCfop", SqlDbType.VarChar).Value = item.Cfop;
                cmd.Parameters.AddWithValue("@pTipoIcms", SqlDbType.VarChar).Value = item.TipoIcms;
                cmd.Parameters.AddWithValue("@pBaseIcms", SqlDbType.Decimal).Value = item.BaseIcms;
                cmd.Parameters.AddWithValue("@pAliquotaIcms", SqlDbType.Decimal).Value = item.AliquotaIcms;
                cmd.Parameters.AddWithValue("@pValorIcms", SqlDbType.Decimal).Value = item.ValorIcms;
                cmd.Parameters.AddWithValue("@pNomeProduto", SqlDbType.VarChar).Value = item.NomeProduto;
                cmd.Parameters.AddWithValue("@pCodigoProduto", SqlDbType.VarChar).Value = item.CodigoProduto;
                cmd.Parameters.AddWithValue("@pValorIpi", SqlDbType.VarChar).Value = item.ValorIpi;
                cmd.Parameters.AddWithValue("@pDesconto", SqlDbType.Decimal).Value = item.Desconto;
                cmd.Parameters.AddWithValue("@pBaseCalculoIpi", SqlDbType.VarChar).Value = item.BaseCalculoIpi;
                cmd.Parameters.AddWithValue("@pAliquotaIpi", SqlDbType.VarChar).Value = item.AliquotaIpi;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
