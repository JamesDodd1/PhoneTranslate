using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneTranslate.Dictionary
{
    public partial class Dictionary : Form
    {
        private List<WordObject> dictionary;
        private List<Row> rows;


        /// <summary> Initalises a new instance of TranslationList </summary>
        public Dictionary()
        {
            InitializeComponent();

            this.dictionary = new Crud().Read();

            Display();
        }


        /// <summary> Retrieves and displays the translation dictionary </summary>
        private void Display()
        {
            // Reset displayed dictionary list
            this.rows = new List<Row>();
            dictionaryPanel.Controls.Clear();
            dictionaryPanel.Controls.Add(button1); // Temp to display scrolling


            int posY = 0;
            foreach (WordObject translation in this.dictionary)
            {
                Row line = new Row();
                line.Add(translation, posY);

                foreach (Cell cell in line.Items)
                {
                    cell.Label.Click += Row_Click;
                    dictionaryPanel.Controls.Add(cell.Label);
                    dictionaryPanel.Controls.Add(cell.TextBox);
                }

                this.rows.Add(line);

                posY += 20;
            }
        }

        private void Row_Click(object sender, EventArgs e)
        {
            /*
            DictionaryLabel dl = (DictionaryLabel)sender;
            this.rows.ForEach(r =>
            {
                r.Items.ForEach(i => 
                {
                    if (i.Label == dl)
                        r.Selected = !r.Selected;
                    else
                        r.Selected = false;
                });
            });
            */
        }
    }
}
