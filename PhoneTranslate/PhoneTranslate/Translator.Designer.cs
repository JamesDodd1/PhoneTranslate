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
            this.selectFileCombo = new System.Windows.Forms.ComboBox();
            this.dictionaryBtn = new System.Windows.Forms.Button();
            this.swearFilter = new System.Windows.Forms.CheckBox();
            this.msgLbl = new System.Windows.Forms.Label();
            this.reverseTranslate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // inputField
            // 
            this.inputField.BackColor = System.Drawing.Color.White;
            this.inputField.Location = new System.Drawing.Point(74, 67);
            this.inputField.Name = "inputField";
            this.inputField.Size = new System.Drawing.Size(200, 100);
            this.inputField.TabIndex = 1;
            this.inputField.Text = "";
            // 
            // outputField
            // 
            this.outputField.BackColor = System.Drawing.Color.White;
            this.outputField.Location = new System.Drawing.Point(316, 67);
            this.outputField.Name = "outputField";
            this.outputField.ReadOnly = true;
            this.outputField.Size = new System.Drawing.Size(200, 100);
            this.outputField.TabIndex = 2;
            this.outputField.Text = "";
            // 
            // inputLbl
            // 
            this.inputLbl.AutoSize = true;
            this.inputLbl.Location = new System.Drawing.Point(74, 45);
            this.inputLbl.Name = "inputLbl";
            this.inputLbl.Size = new System.Drawing.Size(36, 16);
            this.inputLbl.TabIndex = 0;
            this.inputLbl.Text = "Input";
            // 
            // outputLbl
            // 
            this.outputLbl.AutoSize = true;
            this.outputLbl.Location = new System.Drawing.Point(312, 45);
            this.outputLbl.Name = "outputLbl";
            this.outputLbl.Size = new System.Drawing.Size(47, 16);
            this.outputLbl.TabIndex = 0;
            this.outputLbl.Text = "Output";
            // 
            // translateBtn
            // 
            this.translateBtn.Location = new System.Drawing.Point(74, 215);
            this.translateBtn.Name = "translateBtn";
            this.translateBtn.Size = new System.Drawing.Size(100, 30);
            this.translateBtn.TabIndex = 3;
            this.translateBtn.Text = "Translate";
            this.translateBtn.UseVisualStyleBackColor = true;
            this.translateBtn.Click += new System.EventHandler(this.TranslateBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(316, 215);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(100, 30);
            this.saveBtn.TabIndex = 6;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(422, 215);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(100, 30);
            this.loadBtn.TabIndex = 7;
            this.loadBtn.Text = "Load";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(180, 215);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(100, 30);
            this.clearBtn.TabIndex = 4;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // selectFileCombo
            // 
            this.selectFileCombo.FormattingEnabled = true;
            this.selectFileCombo.Items.AddRange(new object[] {
            "Save 1",
            "Save 2",
            "Save 3",
            "Save 4",
            "Save 5"});
            this.selectFileCombo.Location = new System.Drawing.Point(316, 251);
            this.selectFileCombo.Name = "selectFileCombo";
            this.selectFileCombo.Size = new System.Drawing.Size(206, 24);
            this.selectFileCombo.TabIndex = 8;
            this.selectFileCombo.Text = "Select File";
            this.selectFileCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectFileCombo_KeyPress);
            // 
            // dictionaryBtn
            // 
            this.dictionaryBtn.Location = new System.Drawing.Point(74, 304);
            this.dictionaryBtn.Name = "dictionaryBtn";
            this.dictionaryBtn.Size = new System.Drawing.Size(100, 30);
            this.dictionaryBtn.TabIndex = 9;
            this.dictionaryBtn.Text = "Dictionary";
            this.dictionaryBtn.UseVisualStyleBackColor = true;
            this.dictionaryBtn.Click += new System.EventHandler(this.DictionaryBtn_Click);
            // 
            // swearFilter
            // 
            this.swearFilter.AutoSize = true;
            this.swearFilter.Location = new System.Drawing.Point(78, 257);
            this.swearFilter.Name = "swearFilter";
            this.swearFilter.Size = new System.Drawing.Size(96, 20);
            this.swearFilter.TabIndex = 9;
            this.swearFilter.Text = "Swear Filter";
            this.swearFilter.UseVisualStyleBackColor = true;
            // 
            // msgLbl
            // 
            this.msgLbl.AutoSize = true;
            this.msgLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgLbl.Location = new System.Drawing.Point(74, 174);
            this.msgLbl.Name = "msgLbl";
            this.msgLbl.Size = new System.Drawing.Size(63, 16);
            this.msgLbl.TabIndex = 10;
            this.msgLbl.Text = "Message";
            this.msgLbl.Visible = false;
            // 
            // reverseTranslate
            // 
            this.reverseTranslate.AutoSize = true;
            this.reverseTranslate.Location = new System.Drawing.Point(181, 257);
            this.reverseTranslate.Name = "reverseTranslate";
            this.reverseTranslate.Size = new System.Drawing.Size(130, 20);
            this.reverseTranslate.TabIndex = 11;
            this.reverseTranslate.Text = "Reverse Translate";
            this.reverseTranslate.UseVisualStyleBackColor = true;
            // 
            // Translator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.reverseTranslate);
            this.Controls.Add(this.msgLbl);
            this.Controls.Add(this.dictionaryBtn);
            this.Controls.Add(this.swearFilter);
            this.Controls.Add(this.selectFileCombo);
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
        private System.Windows.Forms.ComboBox selectFileCombo;
        private System.Windows.Forms.Button dictionaryBtn;
        private System.Windows.Forms.CheckBox swearFilter;
        private System.Windows.Forms.Label msgLbl;
        private System.Windows.Forms.CheckBox reverseTranslate;
    }
}

