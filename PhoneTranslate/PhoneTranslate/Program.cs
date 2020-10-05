using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneTranslate
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //this is a comment
            WordObject[] checkForArray = new WordObject[3];

            List<WordObject> checkForList = new List<WordObject>();
            checkForList.Add(new WordObject("idk", "I don't know"));
        }
    }

    public struct WordObject
    {
        public string slangWord;
        public string translatedWord;

        public WordObject(string slang, string translated)
        {
            slangWord = slang;
            translatedWord = translated;
        }
    }
}
