using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneTranslate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void TranslateBtn_Click(object sender, EventArgs e)
        {
            string text = inputField.Text.Trim();

            if (text == "") { MessageBox.Show("Nothing entered"); }
            if (text.Length > 150) { }


            WordReplace wr = new WordReplace();

            inputField.Text = "";
            outputField.Text = wr.WordMatch(text);

            MessageBox.Show("Complete");
        }
    }
}
