
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteImposto.Negocios;
using TesteImposto.Dominio.Entidade;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();

        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
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
            if (validadados()) {  
            PREPARAXML service = new PREPARAXML();

            pedido.EstadoOrigem =this.cmbestOrigem.Text;
            pedido.EstadoDestino =this.cmbEstDestino.Text;
            pedido.NomeCliente = textBoxNomeCliente.Text;

            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            foreach (DataRow row in table.Rows)
            {
                var brid = false;

                string linha = row["Brinde"].ToString();
                if (linha == "") {  brid = false; } else { brid = true; }

                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = brid,
                        CodigoProduto =  row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())            
                    });
            }

            service.EmitirNota(pedido);
            MessageBox.Show("Nota Fiscal Gerada na pasta XML.");
            limpaTela();
            }
            else
            {
                MessageBox.Show("Verifique seus dados.");
            }
        }

        private void limpaTela()
        {
            cmbestOrigem.Text = string.Empty;
            cmbEstDestino.Text = string.Empty;
            textBoxNomeCliente.Text = string.Empty;
            dataGridViewPedidos.DataSource = GetTablePedidos();
        }

        private bool validadados()
        {
            if(string.IsNullOrEmpty(cmbestOrigem.Text)){ return false; }
            if (string.IsNullOrEmpty(cmbEstDestino.Text)) { return false; }
            if (string.IsNullOrEmpty(textBoxNomeCliente.Text)) { return false; }
            DataTable table = (DataTable)dataGridViewPedidos.DataSource;
            if (table.Rows.Count==0){ return false; } // need to improve
            return true;
        }

        
    }
}
