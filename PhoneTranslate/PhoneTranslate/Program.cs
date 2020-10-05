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

           

            //creates the list of slang to translation
            List<WordObject> checkForList = new List<WordObject>();
            Setup(ref checkForList);

            //this is so I can breakpoint
            int breakpoint = 1;
        }

        static void Setup(ref List<WordObject> checklist)
        {
            checklist.Add(new WordObject("idk", "I don't know"));
            checklist.Add(new WordObject("ttfn", "tata for now"));
        }

        static string WordMatch(string inputString, List<WordObject> checklist)
        {
            string output = "";
            output = inputString;

            for (int i = 0; i < checklist.Count(); i++)
            {
                output.Replace(checklist[i].slangWord, checklist[i].translatedWord);
            }
            
            
            return output;
        }


    }

    public struct WordObject
    {
        public string slangWord { get; set; }
        public string translatedWord { get; set; }

        public WordObject(string slang, string translated)
        {
            slangWord = slang;
            translatedWord = translated;
        }
    }
}
