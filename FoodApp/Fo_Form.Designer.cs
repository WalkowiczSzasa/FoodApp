﻿
namespace FoodApp
{
    partial class FoodApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FoodApp));
            this.panel1 = new System.Windows.Forms.Panel();
            this.allergeneListBox = new System.Windows.Forms.ListBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.KijeloltPanel = new System.Windows.Forms.Panel();
            this.move_PictureBox = new System.Windows.Forms.PictureBox();
            this.exitBtn = new System.Windows.Forms.Button();
            this.aktiv_Rendelesek1 = new Aktiv_Rendelesek();
            this.rendeles_felvetel2 = new Rendeles_felvetel();
            this.etel_Ital_felvetel1 = new Etel_Ital_felvetel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.move_PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(173)))), ((int)(((byte)(44)))));
            this.panel1.Controls.Add(this.allergeneListBox);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.KijeloltPanel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 585);
            this.panel1.TabIndex = 0;
            // 
            // allergeneListBox
            // 
            this.allergeneListBox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.allergeneListBox.FormattingEnabled = true;
            this.allergeneListBox.ItemHeight = 17;
            this.allergeneListBox.Location = new System.Drawing.Point(9, 374);
            this.allergeneListBox.Name = "allergeneListBox";
            this.allergeneListBox.Size = new System.Drawing.Size(171, 191);
            this.allergeneListBox.TabIndex = 14;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(38, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.White;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button7.Location = new System.Drawing.Point(0, 249);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(179, 43);
            this.button7.TabIndex = 9;
            this.button7.Text = "Étel/Ital felvétele";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.Location = new System.Drawing.Point(0, 187);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(179, 43);
            this.button3.TabIndex = 5;
            this.button3.Text = "Aktív rendelések";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 44);
            this.panel2.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(108, 8);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Menü";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(0, 128);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 43);
            this.button2.TabIndex = 2;
            this.button2.Text = "Rendelés felvétele";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // KijeloltPanel
            // 
            this.KijeloltPanel.BackColor = System.Drawing.Color.White;
            this.KijeloltPanel.Location = new System.Drawing.Point(177, 128);
            this.KijeloltPanel.Margin = new System.Windows.Forms.Padding(2);
            this.KijeloltPanel.Name = "KijeloltPanel";
            this.KijeloltPanel.Size = new System.Drawing.Size(14, 43);
            this.KijeloltPanel.TabIndex = 1;
            // 
            // move_PictureBox
            // 
            this.move_PictureBox.BackColor = System.Drawing.Color.White;
            this.move_PictureBox.Image = ((System.Drawing.Image)(resources.GetObject("move_PictureBox.Image")));
            this.move_PictureBox.Location = new System.Drawing.Point(188, 0);
            this.move_PictureBox.Name = "move_PictureBox";
            this.move_PictureBox.Size = new System.Drawing.Size(28, 33);
            this.move_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.move_PictureBox.TabIndex = 12;
            this.move_PictureBox.TabStop = false;
            this.move_PictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Fo_Form_MouseDown);
            // 
            // exitBtn
            // 
            this.exitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.Image = ((System.Drawing.Image)(resources.GetObject("exitBtn.Image")));
            this.exitBtn.Location = new System.Drawing.Point(914, 0);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(0);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(46, 42);
            this.exitBtn.TabIndex = 1;
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // aktiv_Rendelesek1
            // 
            this.aktiv_Rendelesek1.BackColor = System.Drawing.Color.White;
            this.aktiv_Rendelesek1.Location = new System.Drawing.Point(188, 0);
            this.aktiv_Rendelesek1.Margin = new System.Windows.Forms.Padding(2);
            this.aktiv_Rendelesek1.Name = "aktiv_Rendelesek1";
            this.aktiv_Rendelesek1.Size = new System.Drawing.Size(772, 585);
            this.aktiv_Rendelesek1.TabIndex = 2;
            // 
            // rendeles_felvetel2
            // 
            this.rendeles_felvetel2.BackColor = System.Drawing.Color.White;
            this.rendeles_felvetel2.Location = new System.Drawing.Point(188, 0);
            this.rendeles_felvetel2.Margin = new System.Windows.Forms.Padding(2);
            this.rendeles_felvetel2.Name = "rendeles_felvetel2";
            this.rendeles_felvetel2.Size = new System.Drawing.Size(772, 584);
            this.rendeles_felvetel2.TabIndex = 1;
            // 
            // etel_Ital_felvetel1
            // 
            this.etel_Ital_felvetel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(173)))), ((int)(((byte)(44)))));
            this.etel_Ital_felvetel1.Location = new System.Drawing.Point(188, 0);
            this.etel_Ital_felvetel1.Name = "etel_Ital_felvetel1";
            this.etel_Ital_felvetel1.Size = new System.Drawing.Size(772, 584);
            this.etel_Ital_felvetel1.TabIndex = 13;
            // 
            // FoodApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(960, 585);
            this.Controls.Add(this.etel_Ital_felvetel1);
            this.Controls.Add(this.move_PictureBox);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.aktiv_Rendelesek1);
            this.Controls.Add(this.rendeles_felvetel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FoodApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FoodApp";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FoodApp_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Fo_Form_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.move_PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel KijeloltPanel;
        private System.Windows.Forms.Button button3;
        private Rendeles_felvetel rendeles_felvetel2;
        private Aktiv_Rendelesek aktiv_Rendelesek1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox move_PictureBox;
        private Etel_Ital_felvetel etel_Ital_felvetel1;
        private System.Windows.Forms.ListBox allergeneListBox;
    }
}