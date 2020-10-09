﻿
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

        List<PotentialToken> tokenList = new List<PotentialToken>();
        //this is because I don't have a contains test set up for tokens.
        string tokenValues = "";


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
            checkForList.Add(new WordObject("idk", "I don't know"));
            checkForList.Add(new WordObject("ttfn", "tata for now"));

            createTokenList();
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
                if (checkForList[i].slangWord == output)
                {
                    output = output.Replace(checkForList[i].slangWord, checkForList[i].translatedWord);
                    break;
                }
            }


            return output;
        }

        /// <summary>
        /// creates a list of Tokens to search through the text for. 
        /// One per letter that is used, and lists the slang relevant to the token inside the token.
        /// </summary>
        public void createTokenList()
        {
            //this is so you can run it again after startup
            tokenList.Clear();
            tokenValues = "";

            for (int i = 0; i < checkForList.Count(); i++)
            {
                //if first letter of slang is not in the tokenValues list then create a new token.
                if (!tokenValues.Contains(checkForList[i].slangWord[0]))
                {
                    tokenList.Add(new PotentialToken(checkForList[i].slangWord[0], i));
                    tokenValues = (tokenValues + checkForList[i].slangWord[0]);

                }
            }
        }


        //newTest

        public string RunReplace(string inputstring)
        {
            //this would be needed if it is not the first run you have made. Probably need to split the potentials tokens up
            createTokenList();

            List<ConfirmToken> replaceList = new List<ConfirmToken>();

            //parse the text 
            parseInput(inputstring);
            //find the ones that need replacing
            confirmPotentialMatches(inputstring, ref replaceList);
            //replace the matches (going from end to start)

            string final = replaceMatches(inputstring, ref replaceList);

            //temp
            return final;
        }




        //WIP - Untested
        /// <summary>
        /// Adds a space to the start of the input (a copy) and sets to lower case.
        /// Adds to the Potentials List in PotentialsToken all the possible matches in the text.
        /// </summary>
        /// <param name="input"></param>
        public void parseInput(string input)
        {
            //search through text for each token
            //if found append the start location found to the potentials list in the token struct

            // as you are looking for space then the token (otherwise you would get to many matches) you need to add a space at the start
            //this should be done on a temporary copy of the input set to all lower case

            //adds a space on the left, I think.
            input = "^ " + input;
            input = input.ToLower();

            for (int i = 0; i < tokenList.Count; i++)
            {
                bool found = true;
                //if it has a token
                while (found)
                {
                    if (input.Contains(" " + tokenList[i].tokenValue))
                    {
                        //add location of first match to potential list
                        int firstLocation = input.IndexOf((" " + tokenList[i].tokenValue));
                        tokenList[i].potentialsList.Add(firstLocation);

                        //remove already checked area from input
                        input = input.Remove(0, (firstLocation + 2));

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
        public void confirmPotentialMatches(string input, ref List<ConfirmToken> confirms)
        {
            //go through the list of the potential tokens, 
            //take the start point, find the string between that and the next empty space
            //compare that string against the referenceList with both sides forced to lower case
            //if it matches, replace and exit to go test next potential.

            //replacements need to be done back to front otherwise the locations would get shifted 
            //to be done front to back you need a shunt value that updates on every replace with the diference in letters between the slang and the replacement
            input = (/*"^ " +*/ input + " ^");
            for (int i = 0; i < tokenList.Count; i++)
            {
                for (int j = 0; j < tokenList[i].potentialsList.Count; j++)
                {
                    //find next whitespace
                    //compare
                    int nextwhitespace = input.IndexOf(" ", tokenList[i].potentialsList[j]);
                    int gapcount =  nextwhitespace - (tokenList[i].potentialsList[j] - 1);
                    string check = input.Substring((tokenList[i].potentialsList[j] -1), gapcount);

                    for (int k = 0; k <= tokenList[i].referenceList[k]; k++)
                    {


                        if (check.Contains(checkForList[tokenList[i].referenceList[k]].slangWord))
                        {
                            //you have a match. now figure out what to do with it
                            //maybe make a change list
                            confirms.Add(new ConfirmToken(tokenList[i].potentialsList[j], tokenList[i].referenceList[k]));
                            break;
                        }
                    }


                }
            }
        }

        public string replaceMatches(string input, ref List<ConfirmToken>  list)
        {
            for(int i = (list.Count - 1); i >=0; i--)
            {
                //do the replacing. you start at -1 from count as that is the end val 
                input = input.Replace(checkForList[list[i].checkListLocation].slangWord, checkForList[list[i].checkListLocation].translatedWord);
                //list[i].locationValue
            }

            return input;
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

    public struct PotentialToken
    {
        public char tokenValue { get; set; }
        public List<int> referenceList;

        //every time you find a potential by matching the token, you put the start location in this list
        public List<int> potentialsList;

        public PotentialToken(char tkV, int listLocation)
        {
            referenceList = new List<int>();
            potentialsList = new List<int>();

            tokenValue = tkV;
            referenceList.Add(listLocation);
        }
    }

    public struct ConfirmToken
    {
        public int locationValue { get; set; }
        public int checkListLocation { get; set; }

        public ConfirmToken (int location, int listval)
        {
            locationValue = location;
            checkListLocation = listval;
        }
    }

}

