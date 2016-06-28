using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Data.OleDb;
using System.Data.SqlClient;



namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    { 

        public string cnxString = new System.Configuration.AppSettingsReader().GetValue("cnxDBNetShoes", typeof(System.String)).ToString();

        public bool EscreveNotaFiscal(NotaFiscal notaFiscal)
        {
            System.IO.StringWriter STW = new System.IO.StringWriter();
            XmlTextWriter xmlTW = null;
            XmlSerializer xmlSerializer = new XmlSerializer(notaFiscal.GetType());
            bool passou = false;

            try
            {
                xmlTW = new XmlTextWriter(STW);
                xmlSerializer.Serialize(xmlTW, notaFiscal);

                var path = new System.Configuration.AppSettingsReader().GetValue("CaminhoXML", typeof(System.String)) + "\\NF_" + notaFiscal.Id.ToString() + ".xml";

                System.IO.FileStream file = System.IO.File.Create(path);
                xmlSerializer.Serialize(file, notaFiscal);

            }
            catch (Exception ex)
            {
                passou = false;
            }
            finally
            {
                STW.Close();
                if (xmlTW != null)
                {

                    xmlTW.Close();
                    passou = true;

                }
            }
            return passou;
        }

        public void PersisteNotaFiscal(NotaFiscal notaFiscal)
        {
            Int32 IdNotaFiscal = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(cnxString))
                {
                    using (SqlCommand cmdNF = new SqlCommand("P_NOTA_FISCAL", con))
                    {
                        cmdNF.CommandType = System.Data.CommandType.StoredProcedure;

                        cmdNF.Parameters.AddWithValue("@pNumeroNotaFiscal", notaFiscal.NumeroNotaFiscal);
                        cmdNF.Parameters.AddWithValue("@pSerie", notaFiscal.Serie);
                        cmdNF.Parameters.AddWithValue("@pNomeCliente", notaFiscal.NomeCliente);
                        cmdNF.Parameters.AddWithValue("@pEstadoDestino", notaFiscal.EstadoDestino);
                        cmdNF.Parameters.AddWithValue("@pEstadoOrigem", notaFiscal.EstadoOrigem);


                        SqlParameter outPutParameter = new SqlParameter();
                        outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                        outPutParameter.ParameterName = "@pId";
                        outPutParameter.Value = 0;
                        outPutParameter.Direction = System.Data.ParameterDirection.InputOutput;
                        cmdNF.Parameters.Add(outPutParameter);

                        con.Open();
                        cmdNF.ExecuteNonQuery();
                        IdNotaFiscal = Int32.Parse(outPutParameter.Value.ToString());
                        con.Close();

                    }

                    if (IdNotaFiscal != 0)
                    {
                        foreach (NotaFiscalItem item in notaFiscal.ItensDaNotaFiscal)
                        {
                            using (SqlCommand cmdNFItem = new SqlCommand("P_NOTA_FISCAL_ITEM", con))
                            {
                                cmdNFItem.CommandType = System.Data.CommandType.StoredProcedure;

                                cmdNFItem.Parameters.AddWithValue("@pId", 0);

                                cmdNFItem.Parameters.AddWithValue("@pIdNotaFiscal", IdNotaFiscal);

                                if (item.Cfop == null) cmdNFItem.Parameters.AddWithValue("@pCfop", DBNull.Value);
                                else cmdNFItem.Parameters.AddWithValue("@pCfop", item.Cfop);

                                if (item.TipoIcms == null) cmdNFItem.Parameters.AddWithValue("@pTipoIcms", DBNull.Value);
                                else cmdNFItem.Parameters.AddWithValue("@pTipoIcms", item.TipoIcms);

                                cmdNFItem.Parameters.AddWithValue("@pBaseIcms", item.BaseIcms);

                                cmdNFItem.Parameters.AddWithValue("@pAliquotaIcms", item.AliquotaIcms);

                                cmdNFItem.Parameters.AddWithValue("@pValorIcms", item.ValorIcms);

                                if (item.NomeProduto == null) cmdNFItem.Parameters.AddWithValue("@pNomeProduto", DBNull.Value);
                                else cmdNFItem.Parameters.AddWithValue("@pNomeProduto", item.NomeProduto);

                                if (item.CodigoProduto == null) cmdNFItem.Parameters.AddWithValue("@pCodigoProduto", DBNull.Value);
                                else cmdNFItem.Parameters.AddWithValue("@pCodigoProduto", item.CodigoProduto);

                                cmdNFItem.Parameters.AddWithValue("@pBaseIPI", item.BaseIPI);

                                cmdNFItem.Parameters.AddWithValue("@pValorIPI", item.ValorIPI);

                                cmdNFItem.Parameters.AddWithValue("@pAliquotaIPI", item.AliquotaIPI);

                                cmdNFItem.Parameters.AddWithValue("@pDesconto", item.Desconto);

                                con.Open();
                                cmdNFItem.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}
