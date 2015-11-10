namespace New_Player
{
    partial class FormChooseFile
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
            this.listBoxPlaylist = new System.Windows.Forms.ListBox();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxPlaylist
            // 
            this.listBoxPlaylist.FormattingEnabled = true;
            this.listBoxPlaylist.Location = new System.Drawing.Point(13, 13);
            this.listBoxPlaylist.Name = "listBoxPlaylist";
            this.listBoxPlaylist.Size = new System.Drawing.Size(510, 238);
            this.listBoxPlaylist.TabIndex = 0;
            this.listBoxPlaylist.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxPlaylist_MouseClick);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 267);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(91, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name of the song";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(448, 262);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // FormChooseFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 288);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.listBoxPlaylist);
            this.Name = "FormChooseFile";
            this.Text = "FormChooseFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxPlaylist;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonBrowse;
    }
}