﻿namespace PhoneTranslate
{
    partial class Form1
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
            this.inputText = new System.Windows.Forms.RichTextBox();
            this.outputText = new System.Windows.Forms.RichTextBox();
            this.inputLbl = new System.Windows.Forms.Label();
            this.outputLbl = new System.Windows.Forms.Label();
            this.translateBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputText
            // 
            this.inputText.Location = new System.Drawing.Point(64, 55);
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(100, 96);
            this.inputText.TabIndex = 0;
            this.inputText.Text = "";
            // 
            // outputText
            // 
            this.outputText.Location = new System.Drawing.Point(271, 55);
            this.outputText.Name = "outputText";
            this.outputText.Size = new System.Drawing.Size(100, 96);
            this.outputText.TabIndex = 1;
            this.outputText.Text = "";
            // 
            // inputLbl
            // 
            this.inputLbl.AutoSize = true;
            this.inputLbl.Location = new System.Drawing.Point(64, 36);
            this.inputLbl.Name = "inputLbl";
            this.inputLbl.Size = new System.Drawing.Size(31, 13);
            this.inputLbl.TabIndex = 2;
            this.inputLbl.Text = "Input";
            // 
            // outputLbl
            // 
            this.outputLbl.AutoSize = true;
            this.outputLbl.Location = new System.Drawing.Point(268, 36);
            this.outputLbl.Name = "outputLbl";
            this.outputLbl.Size = new System.Drawing.Size(39, 13);
            this.outputLbl.TabIndex = 3;
            this.outputLbl.Text = "Output";
            // 
            // translateBtn
            // 
            this.translateBtn.Location = new System.Drawing.Point(67, 187);
            this.translateBtn.Name = "translateBtn";
            this.translateBtn.Size = new System.Drawing.Size(75, 23);
            this.translateBtn.TabIndex = 4;
            this.translateBtn.Text = "Translate";
            this.translateBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.translateBtn);
            this.Controls.Add(this.outputLbl);
            this.Controls.Add(this.inputLbl);
            this.Controls.Add(this.outputText);
            this.Controls.Add(this.inputText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox inputText;
        private System.Windows.Forms.RichTextBox outputText;
        private System.Windows.Forms.Label inputLbl;
        private System.Windows.Forms.Label outputLbl;
        private System.Windows.Forms.Button translateBtn;
    }
}

