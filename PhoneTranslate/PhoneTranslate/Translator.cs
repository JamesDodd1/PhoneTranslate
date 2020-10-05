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
    public partial class Translator : Form
    {
        private SaveLoad sl = new SaveLoad();

        public Translator()
        {
            InitializeComponent();
        }


        /// <summary> Tranlates the text within input and prints the results to output </summary>
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


        /// <summary> Clears textboxes </summary>
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            inputField.Clear();
            outputField.Clear();
        }


        /// <summary> Saves text into a file </summary>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string fileName = @"Save";
            DisplayText dt = new DisplayText
            {
                Input = inputField.Text,
                Output = outputField.Text,
            };


            if (this.sl.Save(fileName, dt))
                MessageBox.Show("Saved");
            else
                MessageBox.Show("Failed");
        }


        /// <summary> Loads text from a file </summary>
        private void LoadBtn_Click(object sender, EventArgs e)
        {
            string fileName = @"Save";
            DisplayText dt = this.sl.Load(fileName);

            if (dt != null)
            {
                inputField.Text = dt.Input;
                outputField.Text = dt.Output;

                MessageBox.Show("Loaded");
            }
            else
            {
                MessageBox.Show("Fail");
            }
        }
    }


    public class DisplayText
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }
}
