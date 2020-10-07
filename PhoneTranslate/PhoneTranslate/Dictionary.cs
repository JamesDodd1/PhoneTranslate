using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneTranslate
{
    /// <summary> Dictionary CRUD class </summary>
    class Dictionary
    {
        private string file, fileName;


        /// <summary> Initalises a new instance of Dictionary </summary>
        public Dictionary() 
        {
            this.fileName = @"Dictionary";
            this.file = @".\..\..\files\" + fileName + @".txt";
        }


        /// <summary> Reads the dictionary </summary>
        /// <returns> A list containing all dictionary translations </returns>
        public List<WordObject> Read()
        {
            List<WordObject> disctionary = null;

            try
            {
                // Reads from dictionary
                using (StreamReader reader = new StreamReader(this.file, Encoding.UTF8))
                {
                    disctionary = new List<WordObject>();
                    string line, swear;

                    // Retrieves all translations
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Ignore blank lines
                        if (line == "")
                            continue;

                        disctionary.Add(new WordObject
                        {
                            slangWord = line,
                            translatedWord = reader.ReadLine(),
                        });
                        swear = reader.ReadLine(); // Will add to word object
                    }
                }
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("{0} variable is null", nameof(this.file));
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("{0} file cannot be found", this.fileName);
            }

            return disctionary;
        }


        public bool Add(string slang, string normal) => Add(slang, normal, normal); // Temp until swear added
        /// <summary> Adds a new translation into the dictionary </summary>
        /// <returns> true if successful, otherwise false </returns>
        public bool Add(string slang, string normal, string swear)
        {
            try
            {
                // Append dictionary
                using (StreamWriter writer = new StreamWriter(this.file, true))
                {
                    writer.WriteLine(slang);
                    writer.WriteLine(normal);
                    writer.WriteLine(swear);
                    writer.WriteLine(); // Spacing line
                }

                return true;
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("{0} variable is null", nameof(this.file));
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show(this.fileName + " file cannot be found");
            }


            return false;
        }

        
        public bool Edit(string slang, string normal, string newSlang, string newNormal) => 
            Edit(slang, normal, normal, newSlang, newNormal, newNormal); // Temp until swear added
        /// <summary> Changes the dictionary definition of a translation </summary>
        /// <returns> true if successful, otherwise false </returns>
        public bool Edit(string slang, string normal, string swear, string newSlang, string newNormal, string newSwear)
        {
            List<WordObject> dictionary = Read();
            WordObject translation = dictionary.Where(d => d.slangWord == slang && d.translatedWord == normal).FirstOrDefault();
            int index = dictionary.IndexOf(translation);

            // Replace found translation
            if (index >= 0)
            {
                dictionary[index] = new WordObject
                {
                    slangWord = newSlang,
                    translatedWord = newNormal,
                    //swearWord = newSwear,
                };

                // Rewrite dictionary file
                Write(dictionary);

                return true;
            }

            return false;
        }


        public bool Remove(string slang, string normal) => Remove(slang, normal, normal); // Temp until swear added
        /// <summary> Removes a translation from the dictionary </summary>
        /// <returns> true if successful, otherwise false </returns>
        public bool Remove(string slang, string normal, string swear)
        {
            List<WordObject> dictionary = Read();

            // Rewrite dictionary when removed
            if (dictionary.RemoveAll(d => d.slangWord == slang && d.translatedWord == normal) == 1)
            {
                Write(dictionary);
                return true;
            }

            return false;
        }


        /// <summary> Write out dictionary translations </summary>
        private void Write(List<WordObject> dictionary)
        {
            try
            {
                // Writes to file
                using (StreamWriter writer = new StreamWriter(this.file))
                {
                    foreach (WordObject translaltion in dictionary)
                    {
                        string translationSwear = translaltion.translatedWord; // Temp until swear added

                        writer.WriteLine(translaltion.slangWord);
                        writer.WriteLine(translaltion.translatedWord);
                        writer.WriteLine(translationSwear);
                        writer.WriteLine(); // Spacing line
                    }
                }
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("{0} variable is null", nameof(this.fileName));
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show(this.fileName + " file cannot be found");
            }
        }
    }
}
