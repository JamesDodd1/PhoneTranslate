using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneTranslate.Dictionary
{
    public class Cell
    {
        public Cell() { }


        /// <summary> Adds a new cell  </summary>
        public void Add(string text, int posX, int posY)
        {
            this.Label = new CellLabel
            {
                Text = text,
                Location = new Point(posX, posY),
            };
            this.Label.Visible = true;


            this.TextBox = new CellTextBox
            {
                Text = text,
                Location = new Point(posX, posY),
            };
            this.TextBox.Visible = false;
        }


        /// <summary> Swap between Label and TextBox </summary>
        public void Swap()
        {
            this.Label.Visible = !this.Label.Visible;
            this.TextBox.Visible = !this.TextBox.Visible;
        }


        public CellLabel Label { get; private set; }
        public CellTextBox TextBox { get; private set; }
    }


    public class CellLabel : Label
    {
        /// <summary> Initalises a new instance of CellLabel </summary>
        public CellLabel()
        {
            // Default settings
            this.Size = new Size(161, 20);
            this.Selected = false;

            // Event controls
            this.Click += CellLabel_Click;
        }


        /// <summary> Alternates the Selected value when Label is single clicked </summary>
        private void CellLabel_Click(object sender, EventArgs e)
        {
            this.Selected = !this.Selected;
        }


        public bool Selected
        {
            get => this.selected;
            set
            {
                this.BackColor = value ? Color.DeepSkyBlue : Color.White;
                this.Invalidate();

                this.selected = value;
            }
        }
        private bool selected;
    }


    public class CellTextBox : TextBox
    {
        /// <summary> Initalises a new instance of CellTextBox </summary>
        public CellTextBox()
        {
            // Default settings
            this.Font = new Font("Arial", 10, FontStyle.Regular);
            this.Size = new Size(161, 20);
        }
    }
}
