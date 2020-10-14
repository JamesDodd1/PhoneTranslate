using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneTranslate.Dictionary
{
    public class DictionaryFactory
    {
        public static Crud Create(string type)
        {
            Crud file = null;

            switch (type)
            {
                case "Dictionary":
                    file = new Crud(@"Dictionary");
                    break;

                case "SwearWords":
                    file = new Crud(@"SwearWords");
                    break;

                default:
                    break;
            }

            return file;
        }
    }


    /// <summary> Dictionary CRUD class </summary>
    public class Crud
    {
        private string file, fileName;


        /// <summary> Initalises a new instance of Crud </summary>
        public Crud(string file) 
        {
            this.fileName = file;
            this.file = @".\..\..\files\" + fileName + @".txt";
        }


        /// <summary> Reads the dictionary </summary>
        /// <returns> A list containing all dictionary translations </returns>
        public List<WordObject> Read()
        {
            List<WordObject> dictionary = null;

            try
            {
                // Reads from dictionary
                using (StreamReader reader = new StreamReader(this.file, Encoding.UTF8))
                {
                    dictionary = new List<WordObject>();
                    string line;

                    // Retrieves all translations
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Ignore blank lines
                        if (line == "")
                            continue;

                        dictionary.Add(new WordObject
                        {
                            SlangWord = line,
                            TranslatedWord = reader.ReadLine(),
                        });
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

            return dictionary;
        }


        /// <summary> Adds a new translation into the dictionary </summary>
        /// <returns> true if successful, otherwise false </returns>
        public bool Add(string word, string translated)
        {
            try
            {
                // Append dictionary
                using (StreamWriter writer = new StreamWriter(this.file, true))
                {
                    writer.WriteLine(word);
                    writer.WriteLine(translated);
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

        
       /// <summary> Changes the dictionary definition of a translation </summary>
        /// <returns> true if successful, otherwise false </returns>
        public bool Edit(string word, string translated, string newWord, string newTranslated)
        {
            List<WordObject> dictionary = Read();
            WordObject translation = dictionary.Where(d => d.SlangWord == word && d.TranslatedWord == translated).FirstOrDefault();
            int index = dictionary.IndexOf(translation);

            // Replace found translation
            if (index >= 0)
            {
                dictionary[index] = new WordObject
                {
                    SlangWord = newWord,
                    TranslatedWord = newTranslated,
                };

                // Rewrite dictionary file
                Write(dictionary);

                return true;
            }

            return false;
        }


        /// <summary> Removes a translation from the dictionary </summary>
        /// <returns> true if successful, otherwise false </returns>
        public bool Remove(string word, string translated)
        {
            List<WordObject> dictionary = Read();

            // Rewrite dictionary when removed
            if (dictionary.RemoveAll(d => d.SlangWord == word && d.TranslatedWord == translated) == 1)
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
                        writer.WriteLine(translaltion.SlangWord);
                        writer.WriteLine(translaltion.TranslatedWord);
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
