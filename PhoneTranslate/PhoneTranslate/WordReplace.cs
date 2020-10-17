using PhoneTranslate.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneTranslate
{
    public class WordReplace
    {
        //creates the list of slang to translation
        List<WordObject> checkForList = new List<WordObject>();
        List<WordObject> swearList = new List<WordObject>();

        List<PotentialToken> tokenList = new List<PotentialToken>();
        List<PotentialToken> swearTokenList = new List<PotentialToken>();
        //this is because I don't have a contains test set up for tokens.
        string tokenValues = "";
        string swearTokenValues = "";


        public WordReplace()
        {
            Setup();
        }


        //this is so I can breakpoint
        int breakpoint = 1;


        /// <summary>
        /// Adds all known slang to the CheckForList, then Runs CreateTokenList
        /// </summary>
        public void Setup()
        {
            Crud dictionaryFile = DictionaryFactory.GetFile("Dictionary");
            checkForList = dictionaryFile.Read();

            Crud swearFile = DictionaryFactory.GetFile("SwearWords");
            swearList = swearFile.Read();

            CreateTokenList(ref tokenList, ref tokenValues, checkForList);
        }


        /// <summary> 
        ///this is the origional test function. It works only if the entire input is a pieice of slang
        ///</summary>
        public string WordMatch(string inputString)
        {
            string output = "";
            output = inputString;



            for (int i = 0; i < checkForList.Count(); i++)
            {
                if (checkForList[i].SlangWord == output)
                {
                    output = output.Replace(checkForList[i].SlangWord, checkForList[i].TranslatedWord);
                    break;
                }
            }


            return output;
        }


        /// <summary>
        /// creates a list of Tokens to search through the text for. 
        /// One per letter that is used, and lists the slang relevant to the token inside the token.
        /// </summary>
        public void CreateTokenList(ref List<PotentialToken> tokens, ref string values, List<WordObject> wordList)
        {
            //this is so you can run it again after startup
            tokens.Clear();
            values = "";

            for (int i = 0; i < wordList.Count(); i++)
            {
                //if first letter of slang is not in the tokenValues list then create a new token.
                if (!values.Contains(wordList[i].SlangWord[0]))
                {
                    tokens.Add(new PotentialToken(wordList[i].SlangWord.ToLower()[0], i));
                    values = (values + wordList[i].SlangWord[0]);
                }
                else
                {
                    tokens[values.IndexOf(wordList[i].SlangWord[0])].ReferenceList.Add(i);
                }
            }
        }


        //newTest
        public string RunReplace(string inputstring, bool swearFilter)
        {
            //this would be needed if it is not the first run you have made. Probably need to split the potentials tokens up
            CreateTokenList(ref tokenList, ref tokenValues, checkForList);

            List<ConfirmToken> replaceList = new List<ConfirmToken>();

            //parse the text 
            ParseInput(inputstring, ref tokenList);
            //find the ones that need replacing
            ConfirmPotentialMatches(inputstring, tokenList, ref replaceList, checkForList);
            //replace the matches (going from end to start)
            string final = ReplaceMatches(inputstring, ref replaceList, checkForList);


            if (swearFilter)
            {
                replaceList.Clear();

                CreateTokenList(ref swearTokenList, ref swearTokenValues, swearList);
                ParseInput(final, ref swearTokenList);
                ConfirmPotentialMatches(final, swearTokenList, ref replaceList, swearList);
                final = ReplaceMatches(final, ref replaceList, swearList);
            }

            //temp
            return final;
        }


        /// <summary>
        /// Adds a space to the start of the input (a copy) and sets to lower case.
        /// Adds to the Potentials List in PotentialsToken all the possible matches in the text.
        /// </summary>
        /// <param name="input"></param>
        public void ParseInput(string input, ref List<PotentialToken> tkl)
        {
            //search through text for each token
            //if found append the start location found to the potentials list in the token struct

            // as you are looking for space then the token (otherwise you would get to many matches) you need to add a space at the start
            //this should be done on a temporary copy of the input set to all lower case

            //adds a space on the left, I think.
            input = input.PadLeft(input.Count() + 1);
            input = input.ToLower();

            for (int i = 0; i < tkl.Count; i++)
            {
                bool found = true;
                int shunt = 0;
                //if it has a token
                while (found)
                {

                    if (input.Contains(" " + tkl[i].TokenValue))
                    {
                        //add location of first match to potential list
                        int firstLocation = input.IndexOf((" " + tkl[i].TokenValue));
                        tkl[i].PotentialsList.Add(firstLocation + shunt);

                        //remove already checked area from input
                        input = input.Remove(0, (firstLocation + 2));
                        shunt = (firstLocation + 2);
                        //do till contains is false

                    }
                    else
                    {
                        found = false;
                    }
                }

            }
        }


        /// <summary>
        /// Checks each PotentialsList in ReplaceToken to see if it exists in CheckForList.
        /// </summary>
        /// <param name="input"></param>
        public void ConfirmPotentialMatches(string input, List<PotentialToken> tkl, ref List<ConfirmToken> confirms, List<WordObject> wordList)
        {
            //go through the list of the potential tokens, 
            //take the start point, find the string between that and the next empty space
            //compare that string against the referenceList with both sides forced to lower case
            //if it matches, replace and exit to go test next potential.

            //replacements need to be done back to front otherwise the locations would get shifted 
            //to be done front to back you need a shunt value that updates on every replace with the diference in letters between the slang and the replacement
            input = (/*"^ " +*/ input + " ^");
            input = input.ToLower();

            for (int i = 0; i < tkl.Count; i++)
            {
                PotentialToken token = tkl[i];

                for (int j = 0; j < token.PotentialsList.Count; j++)
                {
                    int potential = token.PotentialsList[j];

                    //find next whitespace
                    //compare

                    int nextWhiteSpace = input.IndexOf(" ", potential);
                    int gapCount = nextWhiteSpace - (potential - 1);
                    string check = input.Substring(potential, gapCount);

                    //what does this doooooo!?
                    for (int k = 0; k < token.ReferenceList.Count; k++)
                    {
                        int reference = token.ReferenceList[k];
                        if (check.Contains(wordList[reference].SlangWord))
                        {
                            //you have a match. now you make a confirm token and put it in the change list
                            confirms.Add(new ConfirmToken(potential, reference));
                            break;
                        }
                    }
                }
            }
        }


        public string ReplaceMatches(string input, ref List<ConfirmToken> list, List<WordObject> slanglist)
        {
            for (int i = (list.Count - 1); i >= 0; i--)
            {
                ConfirmToken token = list[i];

                //do the replacing. you start at -1 from count as that is the end val 
                //input = input.Replace(slanglist[list[i].CheckListLocation].SlangWord, slanglist[list[i].CheckListLocation].TranslatedWord);

                //take out the slang word
                input = input.Remove(token.LocationValue, slanglist[token.CheckListLocation].SlangWord.Count());
                //fill in the new word
                input = input.Insert(token.LocationValue, slanglist[token.CheckListLocation].TranslatedWord);
            }

            return input;
        }
    }


    public struct WordObject
    {
        public string SlangWord { get; set; }
        public string TranslatedWord { get; set; }

        public WordObject(string slang, string translated)
        {
            SlangWord = slang;
            TranslatedWord = translated;
        }
    }


    public struct PotentialToken
    {
        public char TokenValue { get; set; }
        public List<int> ReferenceList { get; private set; }

        //every time you find a potential by matching the token, you put the start location in this list
        public List<int> PotentialsList { get; private set; }


        public PotentialToken(char tkV, int listLocation)
        {
            ReferenceList = new List<int>();
            PotentialsList = new List<int>();

            TokenValue = tkV;
            ReferenceList.Add(listLocation);
        }
    }


    public struct ConfirmToken
    {
        public int LocationValue { get; private set; }
        public int CheckListLocation { get; private set; }

        public ConfirmToken(int location, int listVal)
        {
            LocationValue = location;
            CheckListLocation = listVal;
        }
    }
}
