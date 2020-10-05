namespace PhoneTranslate
{
    partial class Translator
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
            this.inputField = new System.Windows.Forms.RichTextBox();
            this.outputField = new System.Windows.Forms.RichTextBox();
            this.inputLbl = new System.Windows.Forms.Label();
            this.outputLbl = new System.Windows.Forms.Label();
            this.translateBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.loadBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputField
            // 
            this.inputField.BackColor = System.Drawing.Color.White;
            this.inputField.Location = new System.Drawing.Point(74, 67);
            this.inputField.Name = "inputField";
            this.inputField.Size = new System.Drawing.Size(116, 117);
            this.inputField.TabIndex = 0;
            this.inputField.Text = "";
            // 
            // outputField
            // 
            this.outputField.BackColor = System.Drawing.Color.White;
            this.outputField.Location = new System.Drawing.Point(316, 67);
            this.outputField.Name = "outputField";
            this.outputField.ReadOnly = true;
            this.outputField.Size = new System.Drawing.Size(116, 117);
            this.outputField.TabIndex = 1;
            this.outputField.Text = "";
            // 
            // inputLbl
            // 
            this.inputLbl.AutoSize = true;
            this.inputLbl.Location = new System.Drawing.Point(74, 45);
            this.inputLbl.Name = "inputLbl";
            this.inputLbl.Size = new System.Drawing.Size(36, 16);
            this.inputLbl.TabIndex = 2;
            this.inputLbl.Text = "Input";
            // 
            // outputLbl
            // 
            this.outputLbl.AutoSize = true;
            this.outputLbl.Location = new System.Drawing.Point(312, 45);
            this.outputLbl.Name = "outputLbl";
            this.outputLbl.Size = new System.Drawing.Size(47, 16);
            this.outputLbl.TabIndex = 3;
            this.outputLbl.Text = "Output";
            // 
            // translateBtn
            // 
            this.translateBtn.Location = new System.Drawing.Point(78, 230);
            this.translateBtn.Name = "translateBtn";
            this.translateBtn.Size = new System.Drawing.Size(87, 29);
            this.translateBtn.TabIndex = 4;
            this.translateBtn.Text = "Translate";
            this.translateBtn.UseVisualStyleBackColor = true;
            this.translateBtn.Click += new System.EventHandler(this.TranslateBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(324, 230);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(87, 29);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(324, 265);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(87, 29);
            this.loadBtn.TabIndex = 6;
            this.loadBtn.Text = "Load";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(171, 230);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(87, 29);
            this.clearBtn.TabIndex = 7;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // Translator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.translateBtn);
            this.Controls.Add(this.outputLbl);
            this.Controls.Add(this.inputLbl);
            this.Controls.Add(this.outputField);
            this.Controls.Add(this.inputField);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Translator";
            this.Text = "Translator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox inputField;
        private System.Windows.Forms.RichTextBox outputField;
        private System.Windows.Forms.Label inputLbl;
        private System.Windows.Forms.Label outputLbl;
        private System.Windows.Forms.Button translateBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button clearBtn;
    }
}

