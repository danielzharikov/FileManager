namespace FileManagerv3._0
{
    partial class FormToDownload
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnToStart = new System.Windows.Forms.Button();
            this.btnToStop = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnToOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 190);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(489, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // btnToStart
            // 
            this.btnToStart.Location = new System.Drawing.Point(12, 148);
            this.btnToStart.Name = "btnToStart";
            this.btnToStart.Size = new System.Drawing.Size(75, 23);
            this.btnToStart.TabIndex = 1;
            this.btnToStart.Text = "Start";
            this.btnToStart.UseVisualStyleBackColor = true;
            this.btnToStart.Click += new System.EventHandler(this.btnToStart_Click);
            // 
            // btnToStop
            // 
            this.btnToStop.Location = new System.Drawing.Point(426, 148);
            this.btnToStop.Name = "btnToStop";
            this.btnToStop.Size = new System.Drawing.Size(75, 23);
            this.btnToStop.TabIndex = 2;
            this.btnToStop.Text = "Stop";
            this.btnToStop.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(178, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(242, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Download From: URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Download To: Choose";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(178, 43);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(242, 20);
            this.textBox2.TabIndex = 5;
            // 
            // btnToOpen
            // 
            this.btnToOpen.Location = new System.Drawing.Point(426, 41);
            this.btnToOpen.Name = "btnToOpen";
            this.btnToOpen.Size = new System.Drawing.Size(75, 23);
            this.btnToOpen.TabIndex = 7;
            this.btnToOpen.Text = "Open Folder";
            this.btnToOpen.UseVisualStyleBackColor = true;
            this.btnToOpen.Click += new System.EventHandler(this.btnToOpen_Click);
            // 
            // FormToDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 224);
            this.Controls.Add(this.btnToOpen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnToStop);
            this.Controls.Add(this.btnToStart);
            this.Controls.Add(this.progressBar1);
            this.Name = "FormToDownload";
            this.Text = "FormToDownload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnToStart;
        private System.Windows.Forms.Button btnToStop;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnToOpen;
    }
}