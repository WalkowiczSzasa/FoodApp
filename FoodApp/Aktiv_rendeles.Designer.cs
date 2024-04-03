
namespace FoodApp
{
    partial class Aktiv_rendeles
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.nevLabel = new System.Windows.Forms.Label();
            this.telszamLabel = new System.Windows.Forms.Label();
            this.cimLabel = new System.Windows.Forms.Label();
            this.allapotLabel = new System.Windows.Forms.Label();
            this.arLabel = new System.Windows.Forms.Label();
            this.futarComboBox = new System.Windows.Forms.ComboBox();
            this.idoLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(173)))), ((int)(((byte)(44)))));
            this.panel1.Controls.Add(this.checkBox);
            this.panel1.Location = new System.Drawing.Point(0, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(23, 22);
            this.panel1.TabIndex = 0;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(5, 5);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(15, 14);
            this.checkBox.TabIndex = 0;
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // nevLabel
            // 
            this.nevLabel.AutoSize = true;
            this.nevLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(173)))), ((int)(((byte)(44)))));
            this.nevLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nevLabel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nevLabel.ForeColor = System.Drawing.Color.White;
            this.nevLabel.Location = new System.Drawing.Point(24, 18);
            this.nevLabel.Name = "nevLabel";
            this.nevLabel.Size = new System.Drawing.Size(52, 22);
            this.nevLabel.TabIndex = 1;
            this.nevLabel.Text = "Név";
            this.nevLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // telszamLabel
            // 
            this.telszamLabel.AutoSize = true;
            this.telszamLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(173)))), ((int)(((byte)(44)))));
            this.telszamLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.telszamLabel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.telszamLabel.ForeColor = System.Drawing.Color.White;
            this.telszamLabel.Location = new System.Drawing.Point(34, 43);
            this.telszamLabel.Name = "telszamLabel";
            this.telszamLabel.Size = new System.Drawing.Size(131, 22);
            this.telszamLabel.TabIndex = 2;
            this.telszamLabel.Text = "Telefonszám";
            this.telszamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cimLabel
            // 
            this.cimLabel.AutoSize = true;
            this.cimLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(173)))), ((int)(((byte)(44)))));
            this.cimLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cimLabel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cimLabel.ForeColor = System.Drawing.Color.White;
            this.cimLabel.Location = new System.Drawing.Point(34, 66);
            this.cimLabel.Name = "cimLabel";
            this.cimLabel.Size = new System.Drawing.Size(149, 22);
            this.cimLabel.TabIndex = 3;
            this.cimLabel.Text = "Utca házszám";
            this.cimLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // allapotLabel
            // 
            this.allapotLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.allapotLabel.AutoSize = true;
            this.allapotLabel.BackColor = System.Drawing.Color.Goldenrod;
            this.allapotLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.allapotLabel.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.allapotLabel.ForeColor = System.Drawing.Color.White;
            this.allapotLabel.Location = new System.Drawing.Point(295, 6);
            this.allapotLabel.Name = "allapotLabel";
            this.allapotLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.allapotLabel.Size = new System.Drawing.Size(117, 33);
            this.allapotLabel.TabIndex = 4;
            this.allapotLabel.Text = "Állapot";
            this.allapotLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // arLabel
            // 
            this.arLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.arLabel.AutoSize = true;
            this.arLabel.BackColor = System.Drawing.SystemColors.Control;
            this.arLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.arLabel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.arLabel.ForeColor = System.Drawing.Color.Black;
            this.arLabel.Location = new System.Drawing.Point(377, 43);
            this.arLabel.Name = "arLabel";
            this.arLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.arLabel.Size = new System.Drawing.Size(33, 22);
            this.arLabel.TabIndex = 5;
            this.arLabel.Text = "Ár";
            this.arLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // futarComboBox
            // 
            this.futarComboBox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.futarComboBox.FormattingEnabled = true;
            this.futarComboBox.Location = new System.Drawing.Point(255, 94);
            this.futarComboBox.Name = "futarComboBox";
            this.futarComboBox.Size = new System.Drawing.Size(157, 25);
            this.futarComboBox.Sorted = true;
            this.futarComboBox.TabIndex = 6;
            this.futarComboBox.Text = "Válassz egy futárt";
            // 
            // idoLabel
            // 
            this.idoLabel.AutoSize = true;
            this.idoLabel.BackColor = System.Drawing.SystemColors.Control;
            this.idoLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.idoLabel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.idoLabel.ForeColor = System.Drawing.Color.Black;
            this.idoLabel.Location = new System.Drawing.Point(221, 6);
            this.idoLabel.Name = "idoLabel";
            this.idoLabel.Size = new System.Drawing.Size(68, 22);
            this.idoLabel.TabIndex = 7;
            this.idoLabel.Text = "XX:XX";
            this.idoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Aktiv_rendeles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.idoLabel);
            this.Controls.Add(this.futarComboBox);
            this.Controls.Add(this.arLabel);
            this.Controls.Add(this.allapotLabel);
            this.Controls.Add(this.cimLabel);
            this.Controls.Add(this.telszamLabel);
            this.Controls.Add(this.nevLabel);
            this.Controls.Add(this.panel1);
            this.Name = "Aktiv_rendeles";
            this.Size = new System.Drawing.Size(415, 122);
            this.Load += new System.EventHandler(this.Aktiv_rendeles_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Label nevLabel;
        private System.Windows.Forms.Label telszamLabel;
        private System.Windows.Forms.Label cimLabel;
        private System.Windows.Forms.Label allapotLabel;
        private System.Windows.Forms.Label arLabel;
        private System.Windows.Forms.ComboBox futarComboBox;
        private System.Windows.Forms.Label idoLabel;
    }
}
