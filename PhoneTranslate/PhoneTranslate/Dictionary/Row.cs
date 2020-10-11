using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneTranslate.Dictionary
{
    public class Row
    {
        /// <summary> Initalises a new instance of Row </summary>
        public Row()
        {
            this.Items = new List<Cell>();
            this.Selected = false;
        }


        /// <summary> Adds new row of translations </summary>
        public void Add(WordObject translations, int posY)
        {
            Cell(translations.slangWord, 0, posY);
            Cell(translations.translatedWord, 161, posY);
            Cell(translations.translatedWord, 322, posY);
        }


        /// <summary> Creates a cell for each translation type </summary>
        private void Cell(string text, int posX, int posY)
        {
            Cell cell = new Cell();

            cell.Add(text, posX, posY);
            this.Items.Add(cell);

            cell.Label.Click += Label_Click;
            cell.Label.DoubleClick += Label_DoubleClick;

            cell.TextBox.KeyPress += TextBox_KeyPress;
        }


        /// <summary> Alternate the Selected value when Label is single clicked </summary>
        private void Label_Click(object sender, EventArgs e)
        {
            this.Selected = !this.Selected;
        }


        /// <summary> Swap to TextBoxes when double clicked </summary>
        private void Label_DoubleClick(object sender, EventArgs e)
        {
            this.Items.ForEach(i => i.Swap());
        }


        /// <summary> Save changes when Enter Key pressed </summary>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Selected = true;

                this.Items.ForEach(i => i.Swap());
                // Save changes to dictionary
            }
        }


        public List<Cell> Items { get; private set; }
        public bool Selected
        {
            get => this.selected;
            set
            {
                Items.ForEach(i => i.Label.Selected = value);
                this.selected = value;
            }
        }
        private bool selected;
    }
}
