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
            Application.Run(new Translator());
        }
    }


    public class WordReplace
    {
        //creates the list of slang to translation
        List<WordObject> checkForList = new List<WordObject>();

        public WordReplace()
        {
            Setup(ref checkForList);
        }

        
        //this is so I can breakpoint
        int breakpoint = 1;

        static void Setup(ref List<WordObject> checklist)
        {
            checklist.Add(new WordObject("idk", "I don't know"));
            checklist.Add(new WordObject("ttfn", "tata for now"));
        }

        public string WordMatch(string inputString)
        {
            string output = "";
            output = inputString;

            

            for (int i = 0; i < checkForList.Count(); i++)
            {
                if (checkForList[i].slangWord == output)
                {
                    output = output.Replace(checkForList[i].slangWord, checkForList[i].translatedWord);
                    break;
                }
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
