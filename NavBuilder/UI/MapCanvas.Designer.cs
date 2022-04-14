namespace NavBuilder.UI
{
    partial class MapCanvas
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
            this.picRenderer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picRenderer)).BeginInit();
            this.SuspendLayout();
            // 
            // picRenderer
            // 
            this.picRenderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picRenderer.Location = new System.Drawing.Point(0, 0);
            this.picRenderer.Name = "picRenderer";
            this.picRenderer.Size = new System.Drawing.Size(768, 768);
            this.picRenderer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRenderer.TabIndex = 0;
            this.picRenderer.TabStop = false;
            this.picRenderer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picRenderer_MouseClick);
            // 
            // MapCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.picRenderer);
            this.Name = "MapCanvas";
            this.Size = new System.Drawing.Size(768, 768);
            ((System.ComponentModel.ISupportInitialize)(this.picRenderer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picRenderer;
    }
}
