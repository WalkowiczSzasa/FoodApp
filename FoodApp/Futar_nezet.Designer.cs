
namespace FoodApp
{
    partial class Futar_nezet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Futar_nezet));
            this.futarFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // futarFlowLayoutPanel
            // 
            this.futarFlowLayoutPanel.AutoScroll = true;
            this.futarFlowLayoutPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("futarFlowLayoutPanel.BackgroundImage")));
            this.futarFlowLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.futarFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.futarFlowLayoutPanel.Location = new System.Drawing.Point(0, 61);
            this.futarFlowLayoutPanel.Name = "futarFlowLayoutPanel";
            this.futarFlowLayoutPanel.Size = new System.Drawing.Size(438, 523);
            this.futarFlowLayoutPanel.TabIndex = 40;
            // 
            // Futar_nezet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.futarFlowLayoutPanel);
            this.Name = "Futar_nezet";
            this.Size = new System.Drawing.Size(772, 584);
            this.Load += new System.EventHandler(this.Futar_nezet_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel futarFlowLayoutPanel;
    }
}
