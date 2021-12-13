
namespace Debug_Port_80_Control {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tmrVolume = new System.Windows.Forms.Timer(this.components);
            this.cbEffect = new System.Windows.Forms.ComboBox();
            this.grpEffect = new System.Windows.Forms.GroupBox();
            this.grpCustomSend = new System.Windows.Forms.GroupBox();
            this.grpEffect.SuspendLayout();
            this.grpCustomSend.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(9, 33);
            this.txtInput.Margin = new System.Windows.Forms.Padding(6);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(120, 31);
            this.txtInput.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(141, 33);
            this.btnSend.Margin = new System.Windows.Forms.Padding(6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(150, 31);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // tmrVolume
            // 
            this.tmrVolume.Interval = 1;
            this.tmrVolume.Tick += new System.EventHandler(this.TmrVolume_Tick);
            // 
            // cbEffect
            // 
            this.cbEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEffect.FormattingEnabled = true;
            this.cbEffect.Items.AddRange(new object[] {
            "None",
            "Volume"});
            this.cbEffect.Location = new System.Drawing.Point(9, 33);
            this.cbEffect.Margin = new System.Windows.Forms.Padding(6);
            this.cbEffect.Name = "cbEffect";
            this.cbEffect.Size = new System.Drawing.Size(238, 33);
            this.cbEffect.TabIndex = 1;
            this.cbEffect.SelectedIndexChanged += new System.EventHandler(this.CbEffect_SelectedIndexChanged);
            // 
            // grpEffect
            // 
            this.grpEffect.Controls.Add(this.cbEffect);
            this.grpEffect.Location = new System.Drawing.Point(12, 12);
            this.grpEffect.Name = "grpEffect";
            this.grpEffect.Size = new System.Drawing.Size(257, 100);
            this.grpEffect.TabIndex = 0;
            this.grpEffect.TabStop = false;
            this.grpEffect.Text = "&Effect";
            // 
            // grpCustomSend
            // 
            this.grpCustomSend.Controls.Add(this.txtInput);
            this.grpCustomSend.Controls.Add(this.btnSend);
            this.grpCustomSend.Location = new System.Drawing.Point(275, 12);
            this.grpCustomSend.Name = "grpCustomSend";
            this.grpCustomSend.Size = new System.Drawing.Size(309, 100);
            this.grpCustomSend.TabIndex = 2;
            this.grpCustomSend.TabStop = false;
            this.grpCustomSend.Text = "&Custom Send";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 559);
            this.Controls.Add(this.grpCustomSend);
            this.Controls.Add(this.grpEffect);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debug Port 80 Control";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpEffect.ResumeLayout(false);
            this.grpCustomSend.ResumeLayout(false);
            this.grpCustomSend.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Timer tmrVolume;
        private System.Windows.Forms.ComboBox cbEffect;
        private System.Windows.Forms.GroupBox grpEffect;
        private System.Windows.Forms.GroupBox grpCustomSend;
    }
}

