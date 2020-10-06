
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

        List<ReplaceToken> tokenList = new List<ReplaceToken>();
        //this is because I don't have a contains test set up for tokens.
        string tokenValues = "";


        public WordReplace()
        {
            Setup();

        }


        //this is so I can breakpoint
        int breakpoint = 1;

        public void Setup()
        {
            checkForList.Add(new WordObject("idk", "I don't know"));
            checkForList.Add(new WordObject("ttfn", "tata for now"));

            createTokenList();
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

        public void createTokenList()
        {
            for (int i = 0; i < checkForList.Count(); i++)
            {
                //if first letter of slang is not in the tokenValues list then create a new token.
                if (!tokenValues.Contains(checkForList[i].slangWord[0]))
                {
                    tokenList.Add(new ReplaceToken(checkForList[i].slangWord[0], i));

                }
            }
        }

        //WIP

        public void parseInput(string input)
        {
            //search through text for each token
            //if found append the start location found to the potentials list in the token struct

            // as you are looking for space then the token (otherwise you would get to many matches) you need to add a space at the start
            //this should be done on a temporary copy of the input set to all lower case

            //adds a space on the left, I think.
            input.PadLeft(1);
            input = input.ToLower();

            for(int i = 0; i < tokenList.Count; i++)
            {
                //if it has a token
               if(input.Contains(" " + tokenList[i].tokenValue))
                {
                    //add location to potential list
                }
            }
        }

        public void confirmPotentialMatches()
        {
            //go through the list of the potential tokens, 
            //take the start point, find the string between that and the next empty space
            //compare that string against the referenceList with both sides forced to lower case
            //if it matches, replace and exit to go test next potential.

            //replacements need to be done back to front otherwise the locations would get shifted 
            //to be done front to back you need a shunt value that updates on every replace with the diference in letters between the slang and the replacement

            for (int i = 0; i < tokenList.Count; i++)
            {
                for (int j = 0; j < tokenList[i].potentialsList.Count; j++)
                {
                    //find next whitespace
                    //compare


                    // tokenList[i].potentialsList[j]
                }
            }
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

    public struct ReplaceToken
    {
        public char tokenValue { get; set; }
        public List<int> referenceList;

        //every time you find a potential by matching the token, you put the start location in this list
        public List<int> potentialsList;

        public ReplaceToken(char tkV, int listLocation)
        {
            referenceList = new List<int>();
            potentialsList = new List<int>();

            tokenValue = tkV;
            referenceList.Add(listLocation);
        }
    }

}

