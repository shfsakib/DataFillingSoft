namespace DataFillingSoftDeskApp
{
    partial class preloader
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lbltext = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBg = new System.Windows.Forms.Panel();
            this.progressbar = new System.Windows.Forms.Panel();
            this.progressBg.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Universal Education And Research";
            // 
            // lbltext
            // 
            this.lbltext.AutoSize = true;
            this.lbltext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltext.Location = new System.Drawing.Point(152, 130);
            this.lbltext.Name = "lbltext";
            this.lbltext.Size = new System.Drawing.Size(209, 20);
            this.lbltext.TabIndex = 2;
            this.lbltext.Text = "Loading User Interface...";
            this.lbltext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBg
            // 
            this.progressBg.BackgroundImage = global::DataFillingSoftDeskApp.Properties.Resources.border_01;
            this.progressBg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.progressBg.Controls.Add(this.progressbar);
            this.progressBg.Location = new System.Drawing.Point(62, 189);
            this.progressBg.Name = "progressBg";
            this.progressBg.Size = new System.Drawing.Size(380, 20);
            this.progressBg.TabIndex = 4;
            // 
            // progressbar
            // 
            this.progressbar.BackgroundImage = global::DataFillingSoftDeskApp.Properties.Resources.progressbar_01;
            this.progressbar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.progressbar.Location = new System.Drawing.Point(5, 4);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(70, 12);
            this.progressbar.TabIndex = 5;
            // 
            // preloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DataFillingSoftDeskApp.Properties.Resources.bg_border;
            this.ClientSize = new System.Drawing.Size(510, 270);
            this.Controls.Add(this.progressBg);
            this.Controls.Add(this.lbltext);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "preloader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preloader";
            this.progressBg.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbltext;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel progressBg;
        private System.Windows.Forms.Panel progressbar;
    }
}