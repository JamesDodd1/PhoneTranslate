﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneTranslate.Crud
{
    /// <summary> Dictionary CRUD class </summary>
    public class Translate
    {
        private string file, fileName;


        /// <summary> Initalises a new instance of Crud </summary>
        public Translate(string file) 
        {
            this.fileName = file;
            this.file = @".\..\..\..\PhoneTranslate\files\" + file + @".txt";
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
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("{0} file doesn't exist", this.fileName);
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("{0} variable is null", nameof(this.fileName));
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("{0} file cannot be found", this.fileName);
            }

            return dictionary;
        }


        /// <summary> Adds a new translation into the dictionary </summary>
        /// <returns> true if successful, otherwise false </returns>
        public bool Add(string word, string translation)
        {
            if (word == null || word == "") { return false; }
            if (translation == null || translation == "") { return false; }

            List<WordObject> dictionary = Read();

            // Translation already exists
            if (dictionary.Count(d => d.SlangWord == word && d.TranslatedWord == translation) != 0) 
                return false;

            dictionary.Add(new WordObject { SlangWord = word, TranslatedWord = translation });

            // Order alphabetically and rewite dictionary file
            return Write(dictionary.OrderBy(d => d.SlangWord).ToList()); 
        }

        
        /// <summary> Changes the dictionary definition of a translation </summary>
        /// <returns> true if successful, otherwise false </returns>
        public bool Edit(string word, string translated, string newWord, string newTranslation)
        {
            if (newWord == null || newWord == "") { return false; }
            if (newTranslation == null || newTranslation == "") { return false; }


            // Find index of current translation
            List<WordObject> dictionary = Read();
            WordObject translation = dictionary.Where(d => d.SlangWord == word && d.TranslatedWord == translated).FirstOrDefault();
            int index = dictionary.IndexOf(translation);

            // Replace found translation
            if (index >= 0)
            {
                dictionary[index] = new WordObject
                {
                    SlangWord = newWord,
                    TranslatedWord = newTranslation,
                };

                // Order alphabetically and rewrite dictionary file
                return Write(dictionary.OrderBy(d => d.SlangWord).ToList());
            }

            return false;
        }


        /// <summary> Removes a translation from the dictionary </summary>
        /// <returns> true if successful, otherwise false </returns>
        public bool Remove(string word, string translation)
        {
            if (word == null || word == "") { return false; }
            if (translation == null || translation == "") { return false; }


            List<WordObject> dictionary = Read();

            // Rewrite dictionary when removed
            if (dictionary.RemoveAll(d => d.SlangWord == word && d.TranslatedWord == translation) == 1)
                return Write(dictionary);


            return false;
        }


        /// <summary> Write out dictionary translations </summary>
        /// <returns> true if successful, otherwise false </returns>
        private bool Write(List<WordObject> dictionary)
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

                return true;
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("{0} file doesn't exist", nameof(this.fileName));
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("{0} variable is null", nameof(this.fileName));
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show(this.fileName + " file cannot be found");
            }

            return false;
        }
    }
}
