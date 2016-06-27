namespace TesteImposto
{
    partial class FormImposto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNomeCliente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewPedidos = new System.Windows.Forms.DataGridView();
            this.buttonGerarNotaFiscal = new System.Windows.Forms.Button();
            this.cbEstadoOrigem = new System.Windows.Forms.ComboBox();
            this.cbEstadoDestino = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome do Cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Estado Origem";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(551, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Estado Destino";
            // 
            // textBoxNomeCliente
            // 
            this.textBoxNomeCliente.Location = new System.Drawing.Point(6, 28);
            this.textBoxNomeCliente.MaxLength = 50;
            this.textBoxNomeCliente.Name = "textBoxNomeCliente";
            this.textBoxNomeCliente.Size = new System.Drawing.Size(371, 20);
            this.textBoxNomeCliente.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Itens do pedido";
            // 
            // dataGridViewPedidos
            // 
            this.dataGridViewPedidos.AllowUserToOrderColumns = true;
            this.dataGridViewPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPedidos.Location = new System.Drawing.Point(6, 98);
            this.dataGridViewPedidos.Name = "dataGridViewPedidos";
            this.dataGridViewPedidos.Size = new System.Drawing.Size(993, 325);
            this.dataGridViewPedidos.TabIndex = 7;
            this.dataGridViewPedidos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewPedidos_EditingControlShowing);
            // 
            // buttonGerarNotaFiscal
            // 
            this.buttonGerarNotaFiscal.Location = new System.Drawing.Point(872, 440);
            this.buttonGerarNotaFiscal.Name = "buttonGerarNotaFiscal";
            this.buttonGerarNotaFiscal.Size = new System.Drawing.Size(127, 23);
            this.buttonGerarNotaFiscal.TabIndex = 8;
            this.buttonGerarNotaFiscal.Text = "Gerar Nota Fiscal";
            this.buttonGerarNotaFiscal.UseVisualStyleBackColor = true;
            this.buttonGerarNotaFiscal.Click += new System.EventHandler(this.buttonGerarNotaFiscal_Click);
            // 
            // cbEstadoOrigem
            // 
            this.cbEstadoOrigem.CausesValidation = false;
            this.cbEstadoOrigem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstadoOrigem.FormattingEnabled = true;
            this.cbEstadoOrigem.Items.AddRange(new object[] {
            "SP",
            "MG"});
            this.cbEstadoOrigem.Location = new System.Drawing.Point(405, 28);
            this.cbEstadoOrigem.Name = "cbEstadoOrigem";
            this.cbEstadoOrigem.Size = new System.Drawing.Size(121, 21);
            this.cbEstadoOrigem.TabIndex = 9;
            // 
            // cbEstadoDestino
            // 
            this.cbEstadoDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEstadoDestino.FormattingEnabled = true;
            this.cbEstadoDestino.Items.AddRange(new object[] {
            "MG",
            "PA",
            "PB",
            "PE",
            "PI",
            "PR",
            "RJ",
            "RO",
            "SE",
            "TO"});
            this.cbEstadoDestino.Location = new System.Drawing.Point(554, 28);
            this.cbEstadoDestino.Name = "cbEstadoDestino";
            this.cbEstadoDestino.Size = new System.Drawing.Size(121, 21);
            this.cbEstadoDestino.TabIndex = 10;
            // 
            // FormImposto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 475);
            this.Controls.Add(this.cbEstadoDestino);
            this.Controls.Add(this.cbEstadoOrigem);
            this.Controls.Add(this.buttonGerarNotaFiscal);
            this.Controls.Add(this.dataGridViewPedidos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxNomeCliente);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormImposto";
            this.Text = "Calculo de imposto";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNomeCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewPedidos;
        private System.Windows.Forms.Button buttonGerarNotaFiscal;
        private System.Windows.Forms.ComboBox cbEstadoOrigem;
        private System.Windows.Forms.ComboBox cbEstadoDestino;
    }
}

