using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PhoneTranslate.Crud
{
    class SaveLoad
    {
        private string filePath, fileType;

        
        /// <summary> Initalises a new instance of SaveLoad </summary>
        public SaveLoad() 
        {
            this.filePath = @".\..\..\..\PhoneTranslate\files\";
            this.fileType = @".txt";
        }


        /// <summary> Loads up text from an external file </summary>
        /// <returns> If found, an object containing both input and output text, otherwise null </returns>
        public DisplayText Load(string fileName)
        {
            string file = this.filePath + fileName + this.fileType;
            string input = "", output = "";

            try
            {
                // Reads file
                using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
                {
                    input = reader.ReadLine();
                    reader.ReadLine(); // Spacing line
                    output = reader.ReadLine();
                }

                return new DisplayText
                {
                    Input = input,
                    Output = output,
                };
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("{0} variable is null", nameof(fileName));
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show(fileName + " file cannot be found");
            }

            return null;
        }


        /// <summary> Saves text into an external file </summary>
        /// <returns> true when successful, otherwise false </returns>
        public bool Save(string fileName, DisplayText text)
        {
            string file = this.filePath + fileName + this.fileType;

            try
            {
                // Writes to file
                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.WriteLine(text.Input);
                    writer.WriteLine(); // Spacing line
                    writer.WriteLine(text.Output);
                }

                return true;
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("{0} variable is null", nameof(fileName));
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show(fileName + " file cannot be found");
            }

            return false;
        }
    }
}
