namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBoxColor = new Emgu.CV.UI.ImageBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.moutionImageBox = new Emgu.CV.UI.ImageBox();
            this.trackBarRed = new System.Windows.Forms.TrackBar();
            this.trackBarGreen = new System.Windows.Forms.TrackBar();
            this.trackBarBlue = new System.Windows.Forms.TrackBar();
            this.labelRed = new System.Windows.Forms.Label();
            this.labelGreen = new System.Windows.Forms.Label();
            this.labelBlue = new System.Windows.Forms.Label();
            this.labelCenter = new System.Windows.Forms.Label();
            this.labelDebagLog = new System.Windows.Forms.Label();
            this.radioButtonOrange = new System.Windows.Forms.RadioButton();
            this.radioButtonSkin = new System.Windows.Forms.RadioButton();
            this.radioButtonOther = new System.Windows.Forms.RadioButton();
            this.textBoxRed = new System.Windows.Forms.TextBox();
            this.textBoxGreen = new System.Windows.Forms.TextBox();
            this.textBoxBlue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moutionImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxColor
            // 
            this.imageBoxColor.Location = new System.Drawing.Point(12, 12);
            this.imageBoxColor.Name = "imageBoxColor";
            this.imageBoxColor.Size = new System.Drawing.Size(600, 450);
            this.imageBoxColor.TabIndex = 2;
            this.imageBoxColor.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(1091, 483);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(127, 41);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // moutionImageBox
            // 
            this.moutionImageBox.Location = new System.Drawing.Point(618, 12);
            this.moutionImageBox.Name = "moutionImageBox";
            this.moutionImageBox.Size = new System.Drawing.Size(600, 450);
            this.moutionImageBox.TabIndex = 2;
            this.moutionImageBox.TabStop = false;
            // 
            // trackBarRed
            // 
            this.trackBarRed.Location = new System.Drawing.Point(12, 468);
            this.trackBarRed.Maximum = 230;
            this.trackBarRed.Minimum = 25;
            this.trackBarRed.Name = "trackBarRed";
            this.trackBarRed.Size = new System.Drawing.Size(600, 45);
            this.trackBarRed.SmallChange = 5;
            this.trackBarRed.TabIndex = 5;
            this.trackBarRed.Value = 25;
            this.trackBarRed.Scroll += new System.EventHandler(this.trackBarRed_Scroll);
            // 
            // trackBarGreen
            // 
            this.trackBarGreen.Location = new System.Drawing.Point(12, 519);
            this.trackBarGreen.Maximum = 230;
            this.trackBarGreen.Minimum = 25;
            this.trackBarGreen.Name = "trackBarGreen";
            this.trackBarGreen.Size = new System.Drawing.Size(600, 45);
            this.trackBarGreen.SmallChange = 5;
            this.trackBarGreen.TabIndex = 6;
            this.trackBarGreen.Value = 25;
            this.trackBarGreen.Scroll += new System.EventHandler(this.trackBarGreen_Scroll);
            // 
            // trackBarBlue
            // 
            this.trackBarBlue.Location = new System.Drawing.Point(12, 570);
            this.trackBarBlue.Maximum = 230;
            this.trackBarBlue.Minimum = 25;
            this.trackBarBlue.Name = "trackBarBlue";
            this.trackBarBlue.Size = new System.Drawing.Size(600, 45);
            this.trackBarBlue.SmallChange = 5;
            this.trackBarBlue.TabIndex = 7;
            this.trackBarBlue.Value = 25;
            this.trackBarBlue.Scroll += new System.EventHandler(this.trackBarBlue_Scroll);
            // 
            // labelRed
            // 
            this.labelRed.AutoSize = true;
            this.labelRed.Location = new System.Drawing.Point(618, 468);
            this.labelRed.Name = "labelRed";
            this.labelRed.Size = new System.Drawing.Size(27, 13);
            this.labelRed.TabIndex = 8;
            this.labelRed.Text = "Red";
            // 
            // labelGreen
            // 
            this.labelGreen.AutoSize = true;
            this.labelGreen.Location = new System.Drawing.Point(618, 519);
            this.labelGreen.Name = "labelGreen";
            this.labelGreen.Size = new System.Drawing.Size(36, 13);
            this.labelGreen.TabIndex = 9;
            this.labelGreen.Text = "Green";
            // 
            // labelBlue
            // 
            this.labelBlue.AutoSize = true;
            this.labelBlue.Location = new System.Drawing.Point(618, 570);
            this.labelBlue.Name = "labelBlue";
            this.labelBlue.Size = new System.Drawing.Size(28, 13);
            this.labelBlue.TabIndex = 10;
            this.labelBlue.Text = "Blue";
            // 
            // labelCenter
            // 
            this.labelCenter.AutoSize = true;
            this.labelCenter.Location = new System.Drawing.Point(987, 647);
            this.labelCenter.Name = "labelCenter";
            this.labelCenter.Size = new System.Drawing.Size(60, 13);
            this.labelCenter.TabIndex = 11;
            this.labelCenter.Text = "X:           Y:";
            // 
            // labelDebagLog
            // 
            this.labelDebagLog.AutoSize = true;
            this.labelDebagLog.Location = new System.Drawing.Point(12, 647);
            this.labelDebagLog.Name = "labelDebagLog";
            this.labelDebagLog.Size = new System.Drawing.Size(59, 13);
            this.labelDebagLog.TabIndex = 12;
            this.labelDebagLog.Text = "Information";
            // 
            // radioButtonOrange
            // 
            this.radioButtonOrange.AutoSize = true;
            this.radioButtonOrange.Location = new System.Drawing.Point(966, 468);
            this.radioButtonOrange.Name = "radioButtonOrange";
            this.radioButtonOrange.Size = new System.Drawing.Size(60, 17);
            this.radioButtonOrange.TabIndex = 13;
            this.radioButtonOrange.TabStop = true;
            this.radioButtonOrange.Text = "Orange";
            this.radioButtonOrange.UseVisualStyleBackColor = true;
            this.radioButtonOrange.CheckedChanged += new System.EventHandler(this.radioButtonOrange_CheckedChanged);
            // 
            // radioButtonSkin
            // 
            this.radioButtonSkin.AutoSize = true;
            this.radioButtonSkin.Location = new System.Drawing.Point(966, 491);
            this.radioButtonSkin.Name = "radioButtonSkin";
            this.radioButtonSkin.Size = new System.Drawing.Size(46, 17);
            this.radioButtonSkin.TabIndex = 14;
            this.radioButtonSkin.TabStop = true;
            this.radioButtonSkin.Text = "Skin";
            this.radioButtonSkin.UseVisualStyleBackColor = true;
            this.radioButtonSkin.CheckedChanged += new System.EventHandler(this.radioButtonSkin_CheckedChanged);
            // 
            // radioButtonOther
            // 
            this.radioButtonOther.AutoSize = true;
            this.radioButtonOther.Location = new System.Drawing.Point(966, 514);
            this.radioButtonOther.Name = "radioButtonOther";
            this.radioButtonOther.Size = new System.Drawing.Size(51, 17);
            this.radioButtonOther.TabIndex = 15;
            this.radioButtonOther.TabStop = true;
            this.radioButtonOther.Text = "Other";
            this.radioButtonOther.UseVisualStyleBackColor = true;
            this.radioButtonOther.CheckedChanged += new System.EventHandler(this.radioButtonOther_CheckedChanged);
            // 
            // textBoxRed
            // 
            this.textBoxRed.Location = new System.Drawing.Point(826, 468);
            this.textBoxRed.Name = "textBoxRed";
            this.textBoxRed.Size = new System.Drawing.Size(57, 20);
            this.textBoxRed.TabIndex = 16;
            this.textBoxRed.Text = "50";
            this.textBoxRed.TextChanged += new System.EventHandler(this.textBoxRed_TextChanged);
            // 
            // textBoxGreen
            // 
            this.textBoxGreen.Location = new System.Drawing.Point(826, 512);
            this.textBoxGreen.Name = "textBoxGreen";
            this.textBoxGreen.Size = new System.Drawing.Size(57, 20);
            this.textBoxGreen.TabIndex = 17;
            this.textBoxGreen.Text = "50";
            this.textBoxGreen.TextChanged += new System.EventHandler(this.textBoxGreen_TextChanged);
            // 
            // textBoxBlue
            // 
            this.textBoxBlue.Location = new System.Drawing.Point(826, 563);
            this.textBoxBlue.Name = "textBoxBlue";
            this.textBoxBlue.Size = new System.Drawing.Size(57, 20);
            this.textBoxBlue.TabIndex = 18;
            this.textBoxBlue.Text = "50";
            this.textBoxBlue.TextChanged += new System.EventHandler(this.textBoxBlue_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 663);
            this.Controls.Add(this.textBoxBlue);
            this.Controls.Add(this.textBoxGreen);
            this.Controls.Add(this.textBoxRed);
            this.Controls.Add(this.radioButtonOther);
            this.Controls.Add(this.radioButtonSkin);
            this.Controls.Add(this.radioButtonOrange);
            this.Controls.Add(this.labelDebagLog);
            this.Controls.Add(this.labelCenter);
            this.Controls.Add(this.labelBlue);
            this.Controls.Add(this.labelGreen);
            this.Controls.Add(this.labelRed);
            this.Controls.Add(this.trackBarBlue);
            this.Controls.Add(this.trackBarGreen);
            this.Controls.Add(this.trackBarRed);
            this.Controls.Add(this.moutionImageBox);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.imageBoxColor);
            this.Name = "Form1";
            this.Text = "Камера";
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moutionImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBoxColor;
        private System.Windows.Forms.Button btnStart;
        private Emgu.CV.UI.ImageBox moutionImageBox;
        private System.Windows.Forms.TrackBar trackBarRed;
        private System.Windows.Forms.TrackBar trackBarGreen;
        private System.Windows.Forms.TrackBar trackBarBlue;
        private System.Windows.Forms.Label labelRed;
        private System.Windows.Forms.Label labelGreen;
        private System.Windows.Forms.Label labelBlue;
        private System.Windows.Forms.Label labelCenter;
        private System.Windows.Forms.Label labelDebagLog;
        private System.Windows.Forms.RadioButton radioButtonOrange;
        private System.Windows.Forms.RadioButton radioButtonSkin;
        private System.Windows.Forms.RadioButton radioButtonOther;
        private System.Windows.Forms.TextBox textBoxRed;
        private System.Windows.Forms.TextBox textBoxGreen;
        private System.Windows.Forms.TextBox textBoxBlue;

    }
}

