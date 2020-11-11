namespace LR2
{
    partial class Form1
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
            this.checkBoxCompany = new System.Windows.Forms.CheckBox();
            this.comboBoxCompany = new System.Windows.Forms.ComboBox();
            this.checkBoxModel = new System.Windows.Forms.CheckBox();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.comboBoxPrice = new System.Windows.Forms.ComboBox();
            this.checkBoxPrice = new System.Windows.Forms.CheckBox();
            this.comboBoxSize = new System.Windows.Forms.ComboBox();
            this.checkBoxSize = new System.Windows.Forms.CheckBox();
            this.comboBoxQuality = new System.Windows.Forms.ComboBox();
            this.checkBoxQuality = new System.Windows.Forms.CheckBox();
            this.comboBoxRating = new System.Windows.Forms.ComboBox();
            this.checkBoxRating = new System.Windows.Forms.CheckBox();
            this.radioButtonsax = new System.Windows.Forms.RadioButton();
            this.radioButtondom = new System.Windows.Forms.RadioButton();
            this.radioButtonLtx = new System.Windows.Forms.RadioButton();
            this.Find = new System.Windows.Forms.Button();
            this.transform = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // checkBoxCompany
            // 
            this.checkBoxCompany.AutoSize = true;
            this.checkBoxCompany.Location = new System.Drawing.Point(21, 31);
            this.checkBoxCompany.Name = "checkBoxCompany";
            this.checkBoxCompany.Size = new System.Drawing.Size(91, 21);
            this.checkBoxCompany.TabIndex = 0;
            this.checkBoxCompany.Text = "Компанія";
            this.checkBoxCompany.UseVisualStyleBackColor = true;
            // 
            // comboBoxCompany
            // 
            this.comboBoxCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompany.FormattingEnabled = true;
            this.comboBoxCompany.Location = new System.Drawing.Point(127, 29);
            this.comboBoxCompany.Name = "comboBoxCompany";
            this.comboBoxCompany.Size = new System.Drawing.Size(98, 24);
            this.comboBoxCompany.TabIndex = 1;
            // 
            // checkBoxModel
            // 
            this.checkBoxModel.AutoSize = true;
            this.checkBoxModel.Location = new System.Drawing.Point(21, 80);
            this.checkBoxModel.Name = "checkBoxModel";
            this.checkBoxModel.Size = new System.Drawing.Size(80, 21);
            this.checkBoxModel.TabIndex = 2;
            this.checkBoxModel.Text = "Модель";
            this.checkBoxModel.UseVisualStyleBackColor = true;
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(127, 77);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(98, 24);
            this.comboBoxModel.TabIndex = 3;
            // 
            // comboBoxPrice
            // 
            this.comboBoxPrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrice.FormattingEnabled = true;
            this.comboBoxPrice.Location = new System.Drawing.Point(127, 128);
            this.comboBoxPrice.Name = "comboBoxPrice";
            this.comboBoxPrice.Size = new System.Drawing.Size(98, 24);
            this.comboBoxPrice.TabIndex = 5;
            // 
            // checkBoxPrice
            // 
            this.checkBoxPrice.AutoSize = true;
            this.checkBoxPrice.Location = new System.Drawing.Point(21, 131);
            this.checkBoxPrice.Name = "checkBoxPrice";
            this.checkBoxPrice.Size = new System.Drawing.Size(60, 21);
            this.checkBoxPrice.TabIndex = 4;
            this.checkBoxPrice.Text = "Ціна";
            this.checkBoxPrice.UseVisualStyleBackColor = true;
            this.checkBoxPrice.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // comboBoxSize
            // 
            this.comboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSize.FormattingEnabled = true;
            this.comboBoxSize.Location = new System.Drawing.Point(127, 220);
            this.comboBoxSize.Name = "comboBoxSize";
            this.comboBoxSize.Size = new System.Drawing.Size(98, 24);
            this.comboBoxSize.TabIndex = 11;
            this.comboBoxSize.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // checkBoxSize
            // 
            this.checkBoxSize.AutoSize = true;
            this.checkBoxSize.Location = new System.Drawing.Point(21, 223);
            this.checkBoxSize.Name = "checkBoxSize";
            this.checkBoxSize.Size = new System.Drawing.Size(70, 21);
            this.checkBoxSize.TabIndex = 10;
            this.checkBoxSize.Text = "Екран";
            this.checkBoxSize.UseVisualStyleBackColor = true;
            this.checkBoxSize.CheckedChanged += new System.EventHandler(this.checkBoxSize_CheckedChanged);
            // 
            // comboBoxQuality
            // 
            this.comboBoxQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuality.FormattingEnabled = true;
            this.comboBoxQuality.Location = new System.Drawing.Point(127, 264);
            this.comboBoxQuality.Name = "comboBoxQuality";
            this.comboBoxQuality.Size = new System.Drawing.Size(98, 24);
            this.comboBoxQuality.TabIndex = 9;
            // 
            // checkBoxQuality
            // 
            this.checkBoxQuality.AutoSize = true;
            this.checkBoxQuality.Location = new System.Drawing.Point(21, 267);
            this.checkBoxQuality.Name = "checkBoxQuality";
            this.checkBoxQuality.Size = new System.Drawing.Size(71, 21);
            this.checkBoxQuality.TabIndex = 8;
            this.checkBoxQuality.Text = "Якість";
            this.checkBoxQuality.UseVisualStyleBackColor = true;
            // 
            // comboBoxRating
            // 
            this.comboBoxRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRating.FormattingEnabled = true;
            this.comboBoxRating.Location = new System.Drawing.Point(127, 173);
            this.comboBoxRating.Name = "comboBoxRating";
            this.comboBoxRating.Size = new System.Drawing.Size(98, 24);
            this.comboBoxRating.TabIndex = 7;
            // 
            // checkBoxRating
            // 
            this.checkBoxRating.AutoSize = true;
            this.checkBoxRating.Location = new System.Drawing.Point(21, 175);
            this.checkBoxRating.Name = "checkBoxRating";
            this.checkBoxRating.Size = new System.Drawing.Size(83, 21);
            this.checkBoxRating.TabIndex = 6;
            this.checkBoxRating.Text = "Рейтинг";
            this.checkBoxRating.UseVisualStyleBackColor = true;
            // 
            // radioButtonsax
            // 
            this.radioButtonsax.AutoSize = true;
            this.radioButtonsax.Location = new System.Drawing.Point(127, 321);
            this.radioButtonsax.Name = "radioButtonsax";
            this.radioButtonsax.Size = new System.Drawing.Size(56, 21);
            this.radioButtonsax.TabIndex = 12;
            this.radioButtonsax.TabStop = true;
            this.radioButtonsax.Text = "SAX";
            this.radioButtonsax.UseVisualStyleBackColor = true;
            // 
            // radioButtondom
            // 
            this.radioButtondom.AutoSize = true;
            this.radioButtondom.Location = new System.Drawing.Point(21, 321);
            this.radioButtondom.Name = "radioButtondom";
            this.radioButtondom.Size = new System.Drawing.Size(61, 21);
            this.radioButtondom.TabIndex = 13;
            this.radioButtondom.TabStop = true;
            this.radioButtondom.Text = "DOM";
            this.radioButtondom.UseVisualStyleBackColor = true;
            // 
            // radioButtonLtx
            // 
            this.radioButtonLtx.AutoSize = true;
            this.radioButtonLtx.Location = new System.Drawing.Point(21, 359);
            this.radioButtonLtx.Name = "radioButtonLtx";
            this.radioButtonLtx.Size = new System.Drawing.Size(109, 21);
            this.radioButtonLtx.TabIndex = 14;
            this.radioButtonLtx.TabStop = true;
            this.radioButtonLtx.Text = "LINQ to XML";
            this.radioButtonLtx.UseVisualStyleBackColor = true;
            // 
            // Find
            // 
            this.Find.Location = new System.Drawing.Point(404, 23);
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(107, 29);
            this.Find.TabIndex = 15;
            this.Find.Text = "Пошук";
            this.Find.UseVisualStyleBackColor = true;
            this.Find.Click += new System.EventHandler(this.Find_Click);
            // 
            // transform
            // 
            this.transform.Location = new System.Drawing.Point(561, 22);
            this.transform.Name = "transform";
            this.transform.Size = new System.Drawing.Size(125, 30);
            this.transform.TabIndex = 16;
            this.transform.Text = "Трансформація";
            this.transform.UseVisualStyleBackColor = true;
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(258, 23);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(106, 30);
            this.clear.TabIndex = 17;
            this.clear.Text = "Очищення ";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.button3_Click);
            // 
            // rtb
            // 
            this.rtb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.rtb.Location = new System.Drawing.Point(241, 68);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(559, 383);
            this.rtb.TabIndex = 18;
            this.rtb.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtb);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.transform);
            this.Controls.Add(this.Find);
            this.Controls.Add(this.radioButtonLtx);
            this.Controls.Add(this.radioButtondom);
            this.Controls.Add(this.radioButtonsax);
            this.Controls.Add(this.comboBoxSize);
            this.Controls.Add(this.checkBoxSize);
            this.Controls.Add(this.comboBoxQuality);
            this.Controls.Add(this.checkBoxQuality);
            this.Controls.Add(this.comboBoxRating);
            this.Controls.Add(this.checkBoxRating);
            this.Controls.Add(this.comboBoxPrice);
            this.Controls.Add(this.checkBoxPrice);
            this.Controls.Add(this.comboBoxModel);
            this.Controls.Add(this.checkBoxModel);
            this.Controls.Add(this.comboBoxCompany);
            this.Controls.Add(this.checkBoxCompany);
            this.Name = "Form1";
            this.Text = "Лабараторна Робота №2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxCompany;
        private System.Windows.Forms.ComboBox comboBoxCompany;
        private System.Windows.Forms.CheckBox checkBoxModel;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.ComboBox comboBoxPrice;
        private System.Windows.Forms.CheckBox checkBoxPrice;
        private System.Windows.Forms.ComboBox comboBoxSize;
        private System.Windows.Forms.CheckBox checkBoxSize;
        private System.Windows.Forms.ComboBox comboBoxQuality;
        private System.Windows.Forms.CheckBox checkBoxQuality;
        private System.Windows.Forms.ComboBox comboBoxRating;
        private System.Windows.Forms.CheckBox checkBoxRating;
        private System.Windows.Forms.RadioButton radioButtonsax;
        private System.Windows.Forms.RadioButton radioButtondom;
        private System.Windows.Forms.RadioButton radioButtonLtx;
        private System.Windows.Forms.Button Find;
        private System.Windows.Forms.Button transform;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.RichTextBox rtb;
    }
}

