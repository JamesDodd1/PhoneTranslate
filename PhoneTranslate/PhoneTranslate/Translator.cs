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


        /// <summary> Initialises a new instance of Translator </summary>
        public Translator()
        {
            InitializeComponent();
            //new Dictionary.Crud(@"Dictionary").Add("afk", "away from keyboard");
        }


        /// <summary> Tranlates the text within input and prints the results to output </summary>
        private void TranslateBtn_Click(object sender, EventArgs e)
        {
            string text = inputField.Text.Trim();

            if (text == "") { MessageBox.Show("Nothing entered"); }
            if (text.Length > 150) { }


            WordReplace wr = new WordReplace();

            inputField.Text = "";
            //outputField.Text = wr.WordMatch(text);
            outputField.Text = wr.RunReplace(text, swearFilter.Checked);
            

            msgLbl.Text = "Complete";
        }


        /// <summary> Clears textboxes </summary>
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            msgLbl.Text = "";
            inputField.Clear();
            outputField.Clear();
        }


        /// <summary> Saves text into a file </summary>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string fileName = SelectedFile();
            if (fileName == "" || fileName == null)
                return;

            DisplayText dt = new DisplayText
            {
                Input = inputField.Text,
                Output = outputField.Text,
            };
            string msg = "";

            if (this.sl.Save(fileName, dt))
                msg = "Saved";
            else
                msg = "Failed";

            msgLbl.Text = msg;
        }


        /// <summary> Loads text from a file </summary>
        private void LoadBtn_Click(object sender, EventArgs e)
        {
            string fileName = SelectedFile(); // Retrieve selected file's name

            // No file found
            if (fileName == "" || fileName == null) 
                return;

           
            DisplayText text = this.sl.Load(fileName); // Retrieve file's saved data
            string msg = "";

            // Display saved text in TextBoxes
            if (text != null)
            {
                inputField.Text = text.Input;
                outputField.Text = text.Output;

                msg = "Loaded";
            }
            else
                msg = "Fail";

            msgLbl.Text = msg;
        }


        /// <summary> Gets the name of the file selected </summary>
        private string SelectedFile()
        {
            string file = null;

            try
            {
                file = selectFileCombo.SelectedItem.ToString();
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("No file selected");
            }


            return file;
        }


        /// <summary> Disables text from being typed into ComboBox </summary>
        private void SelectFileCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        /// <summary> Display Dictionary form when Button clicked </summary>
        private void DictionaryBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Dictionary.Dictionary().ShowDialog();
            this.Show();
        }
        
            
        private void SwearFilter_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }


    public class DisplayText
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }
}
