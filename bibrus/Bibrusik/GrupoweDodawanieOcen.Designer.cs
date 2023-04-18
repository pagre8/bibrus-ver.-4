namespace Bibrusik
{
    partial class GrupoweDodawanieOcen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrupoweDodawanieOcen));
            this.label4 = new System.Windows.Forms.Label();
            this.semester_combobox = new System.Windows.Forms.ComboBox();
            this.count_checkbox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comment_textbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.category_textbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.weight_combobox = new System.Windows.Forms.ComboBox();
            this.datepicker = new System.Windows.Forms.DateTimePicker();
            this.add_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.label4.Location = new System.Drawing.Point(330, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 68;
            this.label4.Text = "Semestr";
            // 
            // semester_combobox
            // 
            this.semester_combobox.FormattingEnabled = true;
            this.semester_combobox.Items.AddRange(new object[] {
            "1",
            "2"});
            this.semester_combobox.Location = new System.Drawing.Point(333, 112);
            this.semester_combobox.Margin = new System.Windows.Forms.Padding(20);
            this.semester_combobox.Name = "semester_combobox";
            this.semester_combobox.Size = new System.Drawing.Size(91, 21);
            this.semester_combobox.TabIndex = 67;
            this.semester_combobox.Text = "1";
            // 
            // count_checkbox
            // 
            this.count_checkbox.AutoSize = true;
            this.count_checkbox.Checked = true;
            this.count_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.count_checkbox.Location = new System.Drawing.Point(333, 203);
            this.count_checkbox.Name = "count_checkbox";
            this.count_checkbox.Size = new System.Drawing.Size(99, 17);
            this.count_checkbox.TabIndex = 66;
            this.count_checkbox.Text = "Licz do średniej";
            this.count_checkbox.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.label8.Location = new System.Drawing.Point(13, 263);
            this.label8.Margin = new System.Windows.Forms.Padding(15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 62;
            this.label8.Text = "Komentarz";
            // 
            // comment_textbox
            // 
            this.comment_textbox.AcceptsReturn = true;
            this.comment_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comment_textbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.comment_textbox.Location = new System.Drawing.Point(16, 279);
            this.comment_textbox.Margin = new System.Windows.Forms.Padding(20);
            this.comment_textbox.Multiline = true;
            this.comment_textbox.Name = "comment_textbox";
            this.comment_textbox.Size = new System.Drawing.Size(218, 40);
            this.comment_textbox.TabIndex = 65;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.label7.Location = new System.Drawing.Point(330, 143);
            this.label7.Margin = new System.Windows.Forms.Padding(15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "Kategoria";
            // 
            // category_textbox
            // 
            this.category_textbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.category_textbox.Location = new System.Drawing.Point(333, 160);
            this.category_textbox.Margin = new System.Windows.Forms.Padding(20);
            this.category_textbox.Name = "category_textbox";
            this.category_textbox.Size = new System.Drawing.Size(91, 20);
            this.category_textbox.TabIndex = 64;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.label6.Location = new System.Drawing.Point(330, 47);
            this.label6.Margin = new System.Windows.Forms.Padding(15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "waga";
            // 
            // weight_combobox
            // 
            this.weight_combobox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.weight_combobox.FormattingEnabled = true;
            this.weight_combobox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "10"});
            this.weight_combobox.Location = new System.Drawing.Point(333, 64);
            this.weight_combobox.Name = "weight_combobox";
            this.weight_combobox.Size = new System.Drawing.Size(91, 21);
            this.weight_combobox.TabIndex = 63;
            this.weight_combobox.Text = "1";
            // 
            // datepicker
            // 
            this.datepicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker.Location = new System.Drawing.Point(333, 18);
            this.datepicker.Name = "datepicker";
            this.datepicker.Size = new System.Drawing.Size(91, 20);
            this.datepicker.TabIndex = 61;
            // 
            // add_button
            // 
            this.add_button.BackColor = System.Drawing.Color.OldLace;
            this.add_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.add_button.Location = new System.Drawing.Point(349, 296);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(75, 23);
            this.add_button.TabIndex = 74;
            this.add_button.Text = "Dodaj";
            this.add_button.UseVisualStyleBackColor = false;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // GrupoweDodawanieOcen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(445, 337);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.semester_combobox);
            this.Controls.Add(this.count_checkbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comment_textbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.category_textbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.weight_combobox);
            this.Controls.Add(this.datepicker);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GrupoweDodawanieOcen";
            this.Text = "Grupowe dodawnie ocen";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox semester_combobox;
        private System.Windows.Forms.CheckBox count_checkbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox comment_textbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox category_textbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox weight_combobox;
        private System.Windows.Forms.DateTimePicker datepicker;
        private System.Windows.Forms.Button add_button;
    }
}