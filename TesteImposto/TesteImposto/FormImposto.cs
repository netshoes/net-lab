using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using TesteImposto.Business;
using TesteImposto.Entidade;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
        }

        #region [ EVENTOS ]

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            try
            {
                var logica = new NotaFiscalLogica();
                var pedido = new Pedido
                {
                    EstadoOrigem = (string)cbEstadoOrigem.SelectedItem,
                    EstadoDestino = (string)cbEstadoDestino.SelectedItem,
                    NomeCliente = textBoxNomeCliente.Text
                };

                var table = (DataTable)dataGridViewPedidos.DataSource;

                foreach (DataRow row in table.Rows)
                {
                    pedido.ItensDoPedido.Add(
                        new PedidoItem
                        {
                            Brinde = Convert.ToBoolean(!string.IsNullOrEmpty(row["Brinde"].ToString()) && row["Brinde"].Equals(true)),
                            CodigoProduto = row["Codigo do produto"].ToString(),
                            NomeProduto = row["Nome do produto"].ToString(),
                            ValorItemPedido = Convert.ToDouble(!string.IsNullOrEmpty(row["Valor"].ToString()) ? row["Valor"].ToString() : "0")
                        });
                }

                logica.Incluir(pedido);

                LimparCampos();

                MessageBox.Show("Operação efetuada com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewPedidos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                var controle = e.Control as DataGridViewTextBoxEditingControl;
                if (controle != null)
                {
                    controle.Enter -= TirarMascara;
                    controle.Leave -= RetornarMascara;
                    controle.KeyPress -= ApenasValorNumerico;
                    if (((DataGridView)sender).CurrentCell.ColumnIndex == 0)
                    {
                        controle.MaxLength = 50;
                    }
                    if (((DataGridView)sender).CurrentCell.ColumnIndex == 1)
                    {
                        controle.MaxLength = 20;
                    }
                    if (((DataGridView)sender).CurrentCell.ColumnIndex == 2)
                    {
                        controle.MaxLength = 18;
                        controle.Enter += TirarMascara;
                        controle.Leave += RetornarMascara;
                        controle.KeyPress += ApenasValorNumerico;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void RetornarMascara(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                txt.Text = double.Parse(txt.Text.Length == 1 && txt.Text.Equals(",") ? "0" : txt.Text).ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void TirarMascara(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                txt.Text = txt.Text.Replace("R$", "").Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void ApenasValorNumerico(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
                {
                    if (e.KeyChar == ',')
                    {
                        e.Handled = (txt.Text.Contains(","));
                    }
                    else
                        e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region [ MÉTODOS PRIVADOS ]

        private static object GetTablePedidos()
        {
            var table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));

            return table;
        }

        private void ResizeColumns()
        {
            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];

                if (i == 0)
                    coluna.Width = 400;
                else if (i == 1)
                    coluna.Width = 300;
                else if (i == 2)
                    coluna.Width = 200;
                else if (i == 3)
                    coluna.Width = 50;
            }
        }

        private void LimparCampos()
        {
            textBoxNomeCliente.Clear();
            cbEstadoOrigem.SelectedIndex = -1;
            cbEstadoDestino.SelectedIndex = -1;
            dataGridViewPedidos.DataSource = GetTablePedidos();
        }

        #endregion
    }
}
