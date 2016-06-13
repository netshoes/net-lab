using Imposto.Core.Domain;
using Imposto.Core.Service;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();
        private string[] _unidadesFederativas;

        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;
            this.InicializarFormulario();
            ResizeColumns();

            this._unidadesFederativas =
                ConfigurationManager.AppSettings["UnidadesFederativas"]
                .Split(new char[] { '|' });
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth - 10.5);
            }
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));

            return table;
        }

        private void InicializarFormulario()
        {
            textBoxNomeCliente.Text = "Cliente";
            txtEstadoOrigem.Text = "SP";
            txtEstadoDestino.Text = "SP";

            dataGridViewPedidos.DataSource = GetTablePedidos();

            ((DataTable)dataGridViewPedidos.DataSource).Rows.Add(new object[] { "Produto", "Produto", 1M, null });
            ((DataTable)dataGridViewPedidos.DataSource).Rows.Add(new object[] { "Brinde", "Brinde", 0.5M, true });
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            NotaFiscalService service = new NotaFiscalService();

            #region Validações
            if (!this._unidadesFederativas.Contains(txtEstadoOrigem.Text))
            {
                MessageBox.Show("Atenção! Por favor, informe um estado de origem válido.");
                txtEstadoOrigem.Focus();
                txtEstadoOrigem.SelectAll();
                return;
            }

            if (!this._unidadesFederativas.Contains(txtEstadoDestino.Text))
            {
                MessageBox.Show("Atenção! Por favor, informe um estado de destino válido.");
                txtEstadoDestino.Focus();
                txtEstadoDestino.SelectAll();
                return;
            }

            if (dataGridViewPedidos.RowCount <= 1)
            {
                MessageBox.Show("Atenção! Por favor, informe ao menos um produto no pedido.");
                dataGridViewPedidos.Focus();
                return;
            }
            #endregion

            pedido.EstadoOrigem = txtEstadoOrigem.Text;
            pedido.EstadoDestino = txtEstadoDestino.Text;

            pedido.NomeCliente = textBoxNomeCliente.Text;

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            foreach (DataRow row in table.Rows)
            {
                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = !Convert.IsDBNull(row["Brinde"]) && Convert.ToBoolean(row["Brinde"]),
                        CodigoProduto = row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = Convert.ToDouble("0" + row["Valor"].ToString())
                    });
            }

            service.GerarNotaFiscal(pedido);
            MessageBox.Show("Operação efetuada com sucesso");

            this.InicializarFormulario();
        }

        private void FormImposto_Resize(object sender, EventArgs e)
        {
            ResizeColumns();
        }
    }
}
