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
            this.rows = new List<Row>();

            Display();
        }


        /// <summary> Retrieves and displays the translation dictionary </summary>
        private void Display()
        {
            int posY = 0;
            foreach (WordObject translation in this.dictionary)
            {
                Row line = new Row();
                line.Add(translation, posY);

                foreach (Cell cell in line.Items)
                {
                    dictionaryPanel.Controls.Add(cell.Label);
                    dictionaryPanel.Controls.Add(cell.TextBox);
                }

                this.rows.Add(line);

                posY += 20;
            }
        }


        /// <summary> Redraws dictionary panel </summary>
        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            // Reset displayed dictionary list
            this.dictionary = new Crud().Read();
            this.rows = new List<Row>();
            dictionaryPanel.Controls.Clear();
            dictionaryPanel.Controls.Add(button1); // Temp to display scrolling

            Display();
        }


        /// <summary> Changes highlighted rows into editor mode </summary>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            foreach (Row line in rows)
            {
                if (line.Selected)
                    line.Label_DoubleClick(sender, e);
            }
        }


        /// <summary> Saves any changes made to all rows </summary>
        private void SaveAllBtn_Click(object sender, EventArgs e)
        {
            foreach (Row line in rows)
            {
                if (line.Editing)
                    line.Save();
            }

            refreshBtn.PerformClick();
        }
    }
}
