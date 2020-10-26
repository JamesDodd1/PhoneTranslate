using PhoneTranslate.Crud;
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
        private Dictionary<string, string> slangToWord = new Dictionary<string, string>();
        private Dictionary<string, string> swearToFilter = new Dictionary<string, string>();

        private Dictionary<char, PotentialToken> tokenList = new Dictionary<char, PotentialToken>();
        private Dictionary<char, PotentialToken> swearTokenList = new Dictionary<char, PotentialToken>();


        public WordReplace()
        {
            Setup();
        }


        private void Setup()
        {
            List<WordObject> slangList = FileFactory.GetFile("Dictionary").Read();
            List<WordObject> swearList = FileFactory.GetFile("SwearWords").Read();


            for (int i = 0; i < slangList.Count; i++)
            {
                slangToWord.Add(slangList[i].SlangWord, slangList[i].TranslatedWord);
            }


            for (int i = 0; i < swearList.Count; i++)
            {
                swearToFilter.Add(swearList[i].SlangWord, swearList[i].TranslatedWord);
            }
        }


        //The actual replace command to call from outside
        /// <summary>
        /// Replaces text from the input using the dictionary as reference
        /// </summary>
        /// <param name="inputstring"> The text you want to run the replace through </param>
        /// <param name="swearFilter"> Is the swear filter checked </param>
        /// <param name="reverseTranslate"> is the Reverse translate checked </param>
        /// <returns></returns>
        public string RunReplace(string inputstring, bool swearFilter, bool reverseTranslate)
        {
            List<ConfirmToken> replaceList = new List<ConfirmToken>();

            //this would be needed if it is not the first run you have made. Probably need to split the potentials tokens up
            CreateTokenList(ref tokenList, slangToWord, reverseTranslate);

            //parse the text 
            ParseInput(inputstring, tokenList);

            //find the ones that need replacing
            ConfirmPotentialMatches(inputstring, tokenList, ref replaceList, reverseTranslate); 

            //this is the fix, making sure that you replace from the back, buy ordering the list
            replaceList = ArrangeList(replaceList);

            //replace the matches (going from end to start)
            string final = ReplaceMatches(inputstring, replaceList);


            if (swearFilter)
            {
                replaceList.Clear();
                
                CreateTokenList(ref swearTokenList, swearToFilter, false);
                ParseInput(final, swearTokenList);
                ConfirmPotentialMatches(final, swearTokenList, ref replaceList, false);
                final = ReplaceMatches(final, replaceList);
            }


            return final;
        }


        /// <summary>
        /// creates a list of Tokens to search through the text for. 
        /// One per letter that is used, and lists the slang relevant to the token inside the token.
        /// </summary>
        private void CreateTokenList(ref Dictionary<char, PotentialToken> tokens, Dictionary<string, string> slangToWords, bool convertToSlang)
        {
            //this is so you can run it again after startup
            tokens.Clear();


            char firstLetter;
            KeyValuePair<string, string> translation;

            foreach (KeyValuePair<string, string> word in slangToWords)
            {
                if (convertToSlang)
                {
                    firstLetter = word.Value.ToLower()[0];
                    translation = new KeyValuePair<string, string>(word.Value, word.Key);
                }
                else
                {
                    firstLetter = word.Key.ToLower()[0];
                    translation = word;
                }


                // If dictionary contain character token
                if (tokens.TryGetValue(firstLetter, out PotentialToken token))
                    token.TokenTranslations.Add(translation.Key, translation.Value);
                else
                    tokens.Add(firstLetter, new PotentialToken(translation.Key, translation.Value));
            }
        }


        /// <summary>
        /// Adds a space to the start of the input (a copy) and sets to lower case.
        /// Adds to the Potentials List in PotentialsToken all the possible matches in the text.
        /// </summary>
        /// <param name="input"></param>
        private void ParseInput(string input, Dictionary<char, PotentialToken> tokens)
        {
            //search through text for each token
            //if found append the start location found to the potentials list in the token struct

            // as you are looking for space then the token (otherwise you would get to many matches) you need to add a space at the start
            //this should be done on a temporary copy of the input set to all lower case


            input = input.PadLeft(input.Count() + 1);
            input = input.ToLower();
            input = input.Replace("\n", "\n ");


            string reloadInput = input;
            int shunt = 0;

            foreach (KeyValuePair<char, PotentialToken> token in tokens)
            {
                //if it has a token
                while (input.Contains(" " + token.Key))
                {
                    //add location of first match to potential list
                    int firstLocation = input.IndexOf(" " + token.Key);
                    token.Value.PotentialsList.Add(firstLocation + shunt);

                    //remove already checked area from input
                    input = input.Remove(firstLocation, 2);

                    shunt += 2;
                }

                input = reloadInput;
                shunt = 0;
            }
        }


        /// <summary>
        /// Checks each PotentialsList in ReplaceToken to see if it exists in CheckForList.
        /// </summary>
        /// <param name="input"></param>
        private void ConfirmPotentialMatches(string input, Dictionary<char, PotentialToken> tokens, ref List<ConfirmToken> confirms, bool reversed)
        {
            //go through the list of the potential tokens, 
            //take the start point, find the string between that and the next empty space
            //compare that string against the referenceList with both sides forced to lower case
            //if it matches, replace and exit to go test next potential.

            //replacements need to be done back to front otherwise the locations would get shifted 
            //to be done front to back you need a shunt value that updates on every replace with the diference in letters between the slang and the replacement
            input = input + " ^";
            input = input.ToLower();
            input = input.Replace("\n", "\n ");


            foreach (KeyValuePair<char, PotentialToken> token in tokens)
            {
                for (int j = 0; j < token.Value.PotentialsList.Count; j++)
                {
                    int potential = token.Value.PotentialsList[j];

                    if (reversed)
                    {
                        foreach (KeyValuePair<string, string> translation in token.Value.TokenTranslations)
                        {
                            int checkCount = translation.Key.Count();

                            if (checkCount <= input.Count())
                            {
                                if (input.Count() > (checkCount + potential))
                                {
                                    string check = input.Substring(potential, checkCount);

                                    if (check.Contains(translation.Key.ToLower()))
                                    {
                                        //you have a match. now you make a confirm token and put it in the change list
                                        confirms.Add(new ConfirmToken(potential, translation.Key, translation.Value));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        int nextWhiteSpace = input.IndexOf(" ", potential);
                        int gapCount = nextWhiteSpace - (potential - 1);
                        string check = input.Substring(potential, gapCount);

                        foreach (KeyValuePair<string, string> translation in token.Value.TokenTranslations)
                        {
                            if (check.Contains(translation.Key))
                            {
                                //you have a match. now you make a confirm token and put it in the change list
                                confirms.Add(new ConfirmToken(potential, translation.Key, translation.Value));
                                break;
                            }
                        }
                    }
                }
            }
        }


        private List<ConfirmToken> ArrangeList(List<ConfirmToken> tboList)
        {
            List<ConfirmToken> orderedList = new List<ConfirmToken>();
            while (tboList.Count() > 0)
            {
                int lowest = 777777777;
                int lowestVal = 777777777;
                for (int i = 0; i < tboList.Count(); i++)
                {
                    if (tboList[i].LocationValue < lowest)
                    {
                        lowest = tboList[i].LocationValue;
                        lowestVal = i;
                    }

                    if (i == (tboList.Count() - 1))
                    {
                        orderedList.Add(tboList[lowestVal]);
                        tboList.Remove(tboList[lowestVal]);

                        lowest = 777777777;
                        lowestVal = 777777777;
                    }
                }
            }


            tboList = orderedList;
            return tboList;
        }


        private string ReplaceMatches(string input, List<ConfirmToken> tokens)
        {
            for (int i = (tokens.Count - 1); i >= 0; i--)
            {
                //do the replacing. you start at -1 from count as that is the end val 
                input = input.Replace("\n", "\n "); //this works, but is bad coding, find the reason, think man.

                //take out the slang word
                input = input.Remove(tokens[i].LocationValue, tokens[i].Translation.Key.Count());

                //fill in the new word
                input = input.Insert(tokens[i].LocationValue, tokens[i].Translation.Value);

                input = input.Replace("\n ", "\n"); //this is the fix to the above replace
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
        public Dictionary<string, string> TokenTranslations { get; private set; }

        //every time you find a potential by matching the token, you put the start location in this list
        public List<int> PotentialsList { get; private set; }


        public PotentialToken(string key, string value)
        {
            PotentialsList = new List<int>();
            TokenTranslations = new Dictionary<string, string>();

            TokenTranslations.Add(key, value);
        }
    }


    public struct ConfirmToken
    {
        public int LocationValue { get; private set; }
        public KeyValuePair<string, string> Translation { get; private set; }


        public ConfirmToken(int location, string key, string value)
        {
            LocationValue = location;
            Translation = new KeyValuePair<string, string>(key, value);
        }
    }
}
