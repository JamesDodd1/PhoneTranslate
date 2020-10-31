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

        private Dictionary<char, Translation> tokenList = new Dictionary<char, Translation>();
        private Dictionary<char, Translation> swearTokenList = new Dictionary<char, Translation>();

        private List<PotentialToken> potentialList;
        private List<ConfirmToken> replaceList;


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
                this.slangToWord.Add(slangList[i].SlangWord, slangList[i].TranslatedWord);
            }


            for (int i = 0; i < swearList.Count; i++)
            {
                this.swearToFilter.Add(swearList[i].SlangWord, swearList[i].TranslatedWord);
            }
        }


        //The actual replace command to call from outside
        /// <summary>
        /// Replaces text from the input using the dictionary as reference
        /// </summary>
        /// <param name="input"> The text you want to run the replace through </param>
        /// <param name="swearFilter"> Is the swear filter checked </param>
        /// <param name="reverseTranslate"> is the Reverse translate checked </param>
        /// <returns></returns>
        public string RunReplace(string input, bool swearFilter, bool reverseTranslate)
        {
            this.potentialList = new List<PotentialToken>();
            this.replaceList = new List<ConfirmToken>();
            
            // Slang to word translation
            string output = Replace(input, this.tokenList, this.slangToWord, reverseTranslate);

            // Swear to filer translation
            if (swearFilter)
                output = Replace(input, this.swearTokenList, this.swearToFilter, false);

            return output;
        }


        private string Replace(string input, Dictionary<char, Translation> tokens, Dictionary<string, string> translations, bool reverseTranslate)
        {
            this.replaceList.Clear();

            CreateTokenList(ref tokens, translations, reverseTranslate);

            ParseInput(input, tokens);
            ConfirmPotentialMatches(input, tokens, ref this.replaceList, reverseTranslate);
            this.replaceList = ArrangeList(this.replaceList);

            return ReplaceMatches(input, this.replaceList);
        }


        /// <summary>
        /// creates a list of Tokens to search through the text for. 
        /// One per letter that is used, and lists the slang relevant to the token inside the token.
        /// </summary>
        private void CreateTokenList(ref Dictionary<char, Translation> tokens, Dictionary<string, string> slangToWords, bool convertToSlang)
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
                if (tokens.TryGetValue(firstLetter, out Translation token))
                    token.TokenTranslations.Add(translation.Key, translation.Value);
                else
                    tokens.Add(firstLetter, new Translation(translation.Key, translation.Value));
            }
        }


        /// <summary>
        /// Adds a space to the start of the input (a copy) and sets to lower case.
        /// Adds to the Potentials List in PotentialsToken all the possible matches in the text.
        /// </summary>
        /// <param name="input"></param>
        private void ParseInput(string input, Dictionary<char, Translation> tokens)
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

            foreach (KeyValuePair<char, Translation> token in tokens)
            {
                //if it has a token
                while (input.Contains(" " + token.Key))
                {
                    //add location of first match to potential list
                    int firstLocation = input.IndexOf(" " + token.Key);

                    //token.Value.PotentialsList.Add(firstLocation + shunt);
                    potentialList.Add(new PotentialToken((firstLocation + shunt), token.Key));
                    
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
        private void ConfirmPotentialMatches(string input, Dictionary<char, Translation> tokens, ref List<ConfirmToken> confirms, bool reversed)
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


            foreach (KeyValuePair<char, Translation> token in tokens)
            {
                for (int j = 0; j < this.potentialList.Count; j++)
                {
                    int potential = this.potentialList[j].InputLocation;

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


        private List<ConfirmToken> ArrangeList(List<ConfirmToken> tokens)
        {
            List<ConfirmToken> orderedList = new List<ConfirmToken>();
            while (tokens.Count() > 0)
            {
                int lowest = int.MaxValue;
                int lowestVal = int.MaxValue;
                for (int i = 0; i < tokens.Count(); i++)
                {
                    if (tokens[i].LocationValue < lowest)
                    {
                        lowest = tokens[i].LocationValue;
                        lowestVal = i;
                    }

                    if (i == (tokens.Count() - 1))
                    {
                        orderedList.Add(tokens[lowestVal]);
                        tokens.Remove(tokens[lowestVal]);
                    }
                }
            }

            return orderedList;
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


    public struct Translation
    {
        public Dictionary<string, string> TokenTranslations { get; private set; }

        public Translation(string key, string value)
        {
            TokenTranslations = new Dictionary<string, string>();

            TokenTranslations.Add(key, value);
        }
    }


    public struct PotentialToken
    {
        public int InputLocation { get; private set; }
        public char KeyValue { get; private set; }

        public PotentialToken(int input, char key)
        {
            InputLocation = input;
            KeyValue = key;
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
