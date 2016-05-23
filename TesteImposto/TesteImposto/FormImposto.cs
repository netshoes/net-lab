using Imposto.Core.Domain;
using Imposto.Core.Service;
using System;
using System.Data;
using System.Windows.Forms;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();

        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;
            InicializarGridView();
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private void InicializarGridView()
        {
            dataGridViewPedidos.DataSource = GetTablePedidos();

            ((DataGridViewTextBoxColumn)dataGridViewPedidos.Columns["Nome do produto"]).MaxInputLength = 50;
            ((DataGridViewTextBoxColumn)dataGridViewPedidos.Columns["Codigo do produto"]).MaxInputLength = 20;
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

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            NotaFiscalService service = new NotaFiscalService();
            pedido.EstadoOrigem = txtEstadoOrigem.Text;
            pedido.EstadoDestino = txtEstadoDestino.Text;
            pedido.NomeCliente = txtNomeCliente.Text;

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            foreach (DataRow row in table.Rows)
            {
                var brinde = row["Brinde"] != null && row["Brinde"].ToString() == "True";
                var codigo = row["Codigo do produto"] == null ? string.Empty : row["Codigo do produto"].ToString();
                var nome = row["Nome do produto"] == null ? string.Empty : row["Nome do produto"].ToString();
                var valor = row["Valor"] == null ? 0 : Convert.ToDouble(row["Valor"].ToString());

                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = brinde,
                        CodigoProduto = codigo,
                        NomeProduto = nome,
                        ValorItemPedido = valor
                    });
            }

            if (FormularioValido() == false)
                return;

            try
            {
                service.GerarNotaFiscal(pedido);
                LimparCampos();

                MessageBox.Show("Operação efetuada com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível completar a operação - " + ex.Message);
            }
        }

        private bool FormularioValido()
        {
            if (string.IsNullOrEmpty(txtNomeCliente.Text))
            {
                MessageBox.Show("O nome do cliente é obrigatório");
                return false;
            }

            if (string.IsNullOrEmpty(txtEstadoDestino.Text))
            {
                MessageBox.Show("O estado destino é obrigatório");
                return false;
            }

            if (string.IsNullOrEmpty(txtEstadoOrigem.Text))
            {
                MessageBox.Show("O estado origem é obrigatório");
                return false;
            }

            var table = (DataTable)dataGridViewPedidos.DataSource;

            foreach (DataRow row in table.Rows)
            {
                var brinde = row["Brinde"].ToString();
                var codigo = row["Codigo do produto"] == null ? string.Empty : row["Codigo do produto"].ToString();
                var nome = row["Nome do produto"] == null ? string.Empty : row["Nome do produto"].ToString();
                var valor = row["Valor"] == null ? string.Empty : row["Valor"].ToString();

                if (string.IsNullOrEmpty(brinde) && string.IsNullOrEmpty(codigo) && string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(valor))
                    continue;

                if (string.IsNullOrEmpty(nome))
                {
                    MessageBox.Show("O nome do ítem é obrigatório");
                    return false;
                }

                if (string.IsNullOrEmpty(codigo))
                {
                    MessageBox.Show("O código do ítem é obrigatório");
                    return false;
                }

                double valorDouble;
                if (double.TryParse(valor, out valorDouble) == false)
                {
                    MessageBox.Show(string.Format("O campo valor do item {0} deve conter um valor número válido", nome));
                    return false;
                }
            }

            return true;
        }

        private void LimparCampos()
        {
            txtEstadoOrigem.Text = string.Empty;
            txtEstadoDestino.Text = string.Empty;
            txtNomeCliente.Text = string.Empty;

            InicializarGridView();
        }
    }
}
