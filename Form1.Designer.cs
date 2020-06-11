namespace FixBill2
{
    partial class mainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.inDirTextBox = new System.Windows.Forms.TextBox();
            this.selectInDirButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.outDirTextBox = new System.Windows.Forms.TextBox();
            this.selectOutDirButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.unzipCheckBox = new System.Windows.Forms.CheckBox();
            this.replaceSignCheckBox = new System.Windows.Forms.CheckBox();
            this.deleteReasonCheckBox = new System.Windows.Forms.CheckBox();
            this.startFixButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.PayWithDebtCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.inDirTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.selectInDirButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.outDirTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.selectOutDirButton, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.startFixButton, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(543, 227);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Папка с файлами для обработки:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inDirTextBox
            // 
            this.inDirTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inDirTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inDirTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inDirTextBox.Location = new System.Drawing.Point(3, 31);
            this.inDirTextBox.Name = "inDirTextBox";
            this.inDirTextBox.ReadOnly = true;
            this.inDirTextBox.Size = new System.Drawing.Size(374, 22);
            this.inDirTextBox.TabIndex = 1;
            // 
            // selectInDirButton
            // 
            this.selectInDirButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectInDirButton.Location = new System.Drawing.Point(383, 31);
            this.selectInDirButton.Name = "selectInDirButton";
            this.selectInDirButton.Size = new System.Drawing.Size(157, 22);
            this.selectInDirButton.TabIndex = 2;
            this.selectInDirButton.Text = "Выбрать";
            this.selectInDirButton.UseVisualStyleBackColor = true;
            this.selectInDirButton.Click += new System.EventHandler(this.SelectInDirButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Папка для обработанных файлов:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // outDirTextBox
            // 
            this.outDirTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outDirTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outDirTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.outDirTextBox.Location = new System.Drawing.Point(3, 87);
            this.outDirTextBox.Name = "outDirTextBox";
            this.outDirTextBox.ReadOnly = true;
            this.outDirTextBox.Size = new System.Drawing.Size(374, 22);
            this.outDirTextBox.TabIndex = 4;
            // 
            // selectOutDirButton
            // 
            this.selectOutDirButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectOutDirButton.Location = new System.Drawing.Point(383, 87);
            this.selectOutDirButton.Name = "selectOutDirButton";
            this.selectOutDirButton.Size = new System.Drawing.Size(157, 22);
            this.selectOutDirButton.TabIndex = 5;
            this.selectOutDirButton.Text = "Выбрать";
            this.selectOutDirButton.UseVisualStyleBackColor = true;
            this.selectOutDirButton.Click += new System.EventHandler(this.SelectOutDirButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PayWithDebtCheckBox);
            this.groupBox1.Controls.Add(this.unzipCheckBox);
            this.groupBox1.Controls.Add(this.replaceSignCheckBox);
            this.groupBox1.Controls.Add(this.deleteReasonCheckBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 109);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Исправления:";
            // 
            // unzipCheckBox
            // 
            this.unzipCheckBox.AutoSize = true;
            this.unzipCheckBox.Location = new System.Drawing.Point(3, 86);
            this.unzipCheckBox.Name = "unzipCheckBox";
            this.unzipCheckBox.Size = new System.Drawing.Size(153, 17);
            this.unzipCheckBox.TabIndex = 2;
            this.unzipCheckBox.Text = "Извлечь счета из архива";
            this.unzipCheckBox.UseVisualStyleBackColor = true;
            this.unzipCheckBox.CheckedChanged += new System.EventHandler(this.UnzipCheckBox_CheckedChanged);
            // 
            // replaceSignCheckBox
            // 
            this.replaceSignCheckBox.AutoSize = true;
            this.replaceSignCheckBox.Location = new System.Drawing.Point(3, 39);
            this.replaceSignCheckBox.Name = "replaceSignCheckBox";
            this.replaceSignCheckBox.Size = new System.Drawing.Size(194, 17);
            this.replaceSignCheckBox.TabIndex = 1;
            this.replaceSignCheckBox.Text = "Заменить подпись руководителя";
            this.replaceSignCheckBox.UseVisualStyleBackColor = true;
            this.replaceSignCheckBox.CheckedChanged += new System.EventHandler(this.ReplaceSignCheckBox_CheckedChanged);
            // 
            // deleteReasonCheckBox
            // 
            this.deleteReasonCheckBox.AutoSize = true;
            this.deleteReasonCheckBox.Location = new System.Drawing.Point(3, 16);
            this.deleteReasonCheckBox.Name = "deleteReasonCheckBox";
            this.deleteReasonCheckBox.Size = new System.Drawing.Size(151, 17);
            this.deleteReasonCheckBox.TabIndex = 0;
            this.deleteReasonCheckBox.Text = "Убрать основание счета";
            this.deleteReasonCheckBox.UseVisualStyleBackColor = true;
            this.deleteReasonCheckBox.CheckedChanged += new System.EventHandler(this.DeleteReasonCheckBox_CheckedChanged);
            // 
            // startFixButton
            // 
            this.startFixButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.startFixButton.Location = new System.Drawing.Point(383, 141);
            this.startFixButton.Name = "startFixButton";
            this.startFixButton.Size = new System.Drawing.Size(157, 56);
            this.startFixButton.TabIndex = 7;
            this.startFixButton.Text = "Обработать";
            this.startFixButton.UseVisualStyleBackColor = true;
            this.startFixButton.Click += new System.EventHandler(this.StartFixButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 233);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(537, 16);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 8;
            // 
            // PayWithDebtCheckBox
            // 
            this.PayWithDebtCheckBox.AutoSize = true;
            this.PayWithDebtCheckBox.Location = new System.Drawing.Point(3, 62);
            this.PayWithDebtCheckBox.Name = "PayWithDebtCheckBox";
            this.PayWithDebtCheckBox.Size = new System.Drawing.Size(271, 17);
            this.PayWithDebtCheckBox.TabIndex = 3;
            this.PayWithDebtCheckBox.Text = "Удалить пени и оплату с учетом задолженности";
            this.PayWithDebtCheckBox.UseVisualStyleBackColor = true;
            this.PayWithDebtCheckBox.CheckedChanged += new System.EventHandler(this.PayWithDebtCheckBox_CheckedChanged);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 249);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.progressBar1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainWindow";
            this.Text = "FixBill - обработка счетов из БСТ";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inDirTextBox;
        private System.Windows.Forms.Button selectInDirButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outDirTextBox;
        private System.Windows.Forms.Button selectOutDirButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox unzipCheckBox;
        private System.Windows.Forms.CheckBox replaceSignCheckBox;
        private System.Windows.Forms.CheckBox deleteReasonCheckBox;
        private System.Windows.Forms.Button startFixButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox PayWithDebtCheckBox;
    }
}

